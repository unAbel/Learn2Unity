using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float velocidad = 5.0f;
    public float velocidadRotacion = 250.0f;
    private Animator anim;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener la referencia al componente Animator y Rigidbody
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtén el input del jugador
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Crear un vector de movimiento basado en el input
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical).normalized;
        // Mover al personaje
        rb.MovePosition(transform.position + movimiento * velocidad * Time.deltaTime);
        
        // Rotar al personaje
        if (movimiento != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movimiento, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, velocidadRotacion * Time.deltaTime);
        }

        // Actualizar las animaciones del Blend Tree
        anim.SetFloat("velH", movimientoHorizontal);
        anim.SetFloat("velV", movimientoVertical);
    }
}
