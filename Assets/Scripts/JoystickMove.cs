using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 520f; // grados por segundo
    public Joystick movementJoystick; // Asigna esto en el inspector para Android
    private Animator animator;
    private Rigidbody rb;
    private Vector3 movementInput;
    private float rotationInput;
    private System.Action handleInput;
    float horizontal;
    float vertical;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
#if UNITY_ANDROID
        handleInput = HandleJoystickInput;
#else
        handleInput = HandleKeyboardInput;
#endif
    }

    void Update()
    {
        handleInput();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void HandleKeyboardInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Control de Movimiento
        movementInput = new Vector3(0f, 0f, vertical).normalized * moveSpeed;

        // Control de Rotación
        rotationInput = horizontal * rotationSpeed * Time.deltaTime;

        // Actualizar el Animator
        UpdateAnimator(vertical, horizontal);
    }

    void HandleJoystickInput()
    {

        horizontal = movementJoystick.Direction.x;
        vertical = movementJoystick.Direction.y;

        if (Mathf.Abs(vertical) >= 0.3f || Mathf.Abs(horizontal) >= 0.3f)
        {
            // Normalizar el input para obtener valores de 0 o 1
            float normalizedVertical = vertical;// > 0 ? 1 : (vertical < 0 ? -1 : 0);
            float normalizedHorizontal = horizontal;// > 0 ? 1 : (horizontal < 0 ? -1 : 0);

            movementInput = new Vector3(0f, 0f, normalizedVertical * moveSpeed);

            rotationInput = normalizedHorizontal * rotationSpeed * Time.deltaTime * Mathf.Sign(normalizedVertical);
            //rotationInput = normalizedHorizontal * rotationSpeed * Time.deltaTime;

            // Actualizar el Animator
            UpdateAnimator(normalizedVertical, normalizedHorizontal);
        }
        /*
        if (movementJoystick.Direction.magnitude >= 0.75f)
        {
            //movementInput = new Vector3(movementJoystick.Direction.x * moveSpeed, 0, movementJoystick.Direction.y * moveSpeed);
            movementInput = new Vector3(0f, 0f * moveSpeed, movementJoystick.Direction.y * moveSpeed);
            rotationInput = movementJoystick.Direction.x * rotationSpeed * Time.deltaTime;
            //rotationInput = 0;

            // Actualizar el Animator
            UpdateAnimator(movementJoystick.Direction.y, movementJoystick.Direction.x);
        }*/
        else
        {
            movementInput = Vector3.zero;
            rotationInput = 0;

            // Actualizar el Animator
            UpdateAnimator(0, 0);
        }
    }

    void ApplyMovement()
    {
        // Aplicar movimiento
        if (movementInput.magnitude >= 0.1f)
        {
            Vector3 moveDir = transform.forward * movementInput.z * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + moveDir);
        }

        // Aplicar rotación
        if (rotationInput != 0)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotationInput, 0f));
        }
    }

    void UpdateAnimator(float vertical, float horizontal)
    {
        animator.SetFloat("velV", vertical);
        animator.SetFloat("velH", horizontal);
    }
}



/*
public class JoystickMove : MonoBehaviour
{
    public Joystick movementJoystick;
    public float playerSpeed;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate(){
    

        if (movementJoystick.Direction.y != 0)
        {
            // Convertir el Vector2 a Vector3 para la dirección en el espacio 3D
            Vector3 movement = new Vector3(movementJoystick.Direction.x * playerSpeed, 0, movementJoystick.Direction.y * playerSpeed);
            rb.velocity = movement;
        } else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
*/