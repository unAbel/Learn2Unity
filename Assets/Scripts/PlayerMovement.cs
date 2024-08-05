using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 520f; // grados por segundo
    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Control de Movimiento
        Vector3 direction = new Vector3(0f, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = transform.forward * vertical * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + moveDir);

            // Actualizar el parámetro Speed en el Animator
            animator.SetFloat("velV", vertical);
        }
        else
        {
            animator.SetFloat("velV", 0f);
        }

        // Control de Rotación
        if (horizontal != 0)
        {
            float rotation = horizontal * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotation, 0f);

            // Actualizar el parámetro RotationSpeed en el Animator
            animator.SetFloat("velH", horizontal);
        }
        else
        {
            animator.SetFloat("velH", 0f);
        }

        // Actualizar el Animator para manejar rotación mientras se avanza
        if (vertical != 0)
        {
            if (horizontal != 0)
            {
                animator.SetFloat("velV", 1f);  // Asegurar que esté avanzando
                animator.SetFloat("velH", horizontal);
            }
            else
            {
                animator.SetFloat("velH", 0f);
            }
        }
    }
}
