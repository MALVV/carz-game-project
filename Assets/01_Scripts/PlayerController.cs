using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables de Manejo
    public float speed = 5.0f;
    public float turnSpeed = 0.0f;
    public float horizontalInput;
    public float fowardInput;

    //Variables de camara
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchCameraKey;
    void Start()
    {
        
    }

    void Update()
    {
        //Asignacion de configuraciondes de teclado
        horizontalInput = Input.GetAxis("Horizontal");
        fowardInput = Input.GetAxis("Vertical");

        //Movimiento del vehiculo
        transform.Translate(Vector3.forward * Time.deltaTime * speed * fowardInput);
        //Giro del Vehiculo
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        //Cambio entre camaras
        if(Input.GetKeyDown(switchCameraKey))
        {
            mainCamera.enabled=!mainCamera.enabled;
            hoodCamera.enabled=!hoodCamera.enabled;
        }
    }
}
