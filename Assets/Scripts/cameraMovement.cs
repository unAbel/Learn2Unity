using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform target; // El jugador o el objeto que la cámara debe seguir
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Desplazamiento de la cámara respecto al jugador

    private Vector3 desiredPosition; // La posición deseada de la cámara
    private Vector3 smoothedPosition; // La posición suavizada de la cámara
    private float fixedXRotation; // Almacena la rotación fija en el eje X
    private Vector3 velocity = Vector3.zero; // Velocidad utilizada para SmoothDamp
   
    void LateUpdate()
    {
        // Calcular la posición deseada con el offset
        desiredPosition = target.position + offset;

        // Interpolar suavemente entre la posición actual y la deseada
        //smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Interpolar suavemente entre la posición actual y la deseada utilizando SmoothDamp
        smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Establecer la posición de la cámara
        transform.position = smoothedPosition;

        //transform.LookAt(target);
        // Mantener la rotación fija en el eje X y actualizar solo los otros ejes
        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0; // Mantener la dirección de mirar en el plano horizontal
        Quaternion rotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Euler(fixedXRotation, rotation.eulerAngles.y, rotation.eulerAngles.z);

        // Opcional: si quieres que la cámara siempre mire al objetivo
       // transform.LookAt(target);
    }


    void Start()
    {
        fixedXRotation = transform.eulerAngles.x;
    }
}
