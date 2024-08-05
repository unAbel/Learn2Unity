using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // El transform del jugador
    //public Vector3 offset;    // La distancia entre la cámara y el jugador
    public Vector3 offset = new Vector3(0, 5, -10); 
    public float smoothSpeed = 0.125f;  // La velocidad de suavizado para el movimiento
    public float rotationSpeed = 5f;    // La velocidad de suavizado para la rotación

    void Start()
    {
        // Inicializar el offset basándose en la posición inicial de la cámara y del jugador
       // offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Calcular la posición deseada de la cámara
        Vector3 desiredPosition = player.position + player.forward * offset.z + player.up * offset.y;
        
        // Suavizar el movimiento de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Asegurarse de que la cámara siempre mire en la misma dirección que el jugador
        Quaternion targetRotation = Quaternion.LookRotation(player.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
