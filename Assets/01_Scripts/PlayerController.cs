using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public AudioSource gasSound;
    public AudioSource motorSound;
    public AudioSource finishLineSound;
    public TextMeshProUGUI gameOverText; // Referencia al objeto de texto de UI
    public Image energyBar; // Referencia al Slider en la UI
    public float maxGas = 100; // Cantidad m�xima de gasolina
    public float currentGas=100; // Cantidad actual de gasolina
    public GameObject finishLine; // Referencia al objeto de la l�nea de meta
    private bool gameFinished = false; // Indica si el juego ha terminado

    // Variables de Manejo
    public float horsePower = 10f;
    public float maxSpeed = 40f;
    public float speed = 5.0f;
    public float turnSpeed = 0.0f;
    public float horizontalInput;
    public float forwardInput;

    // Variables de camara
    public Camera mainCamera;
    public Camera hoodCamera;
    public Camera finishCamera;
    public KeyCode switchCameraKey;

    private bool isMoving = false;
    // Obt�n una referencia al componente de audio
   
    void Start()
    {

        gameOverText.enabled = false;
        currentGas = maxGas; // Establece la gasolina inicial
        UpdateEnergyBar();
    }
    void Update()
    {
        if (!gameFinished) // Verifica si el juego no ha terminado
        {
            energyBar.fillAmount = currentGas / maxGas; // Actualiza visualmente la barra de gas

            if (currentGas > 0)
            {
                if (!motorSound.isPlaying)
                {
                    motorSound.Play();
                }
                horizontalInput = Input.GetAxis("Horizontal");
                forwardInput = Input.GetAxis("Vertical");

                // Movimiento del veh�culo
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

                // Giro del Veh�culo
                transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

                // Comprueba si el veh�culo est� en movimiento
                isMoving = forwardInput != 0;

                // Si el veh�culo est� en movimiento, consume gasolina
                if (isMoving)
                {
                    float gasConsumptionRate = 10f; // Ajusta seg�n tus necesidades
                    ConsumeGas(gasConsumptionRate * Time.deltaTime);
                }
              
            }
            else
            {
                motorSound.Stop();
                // Detener el juego si el gas es igual o menor a 0
                FinishGame();
            }

            // Verifica si el auto ha ca�do por debajo de la altura 5
            if (transform.position.y < -20)
            {
                FinishGame(); // Llama a la funci�n para finalizar el juego
            }

            // Cambio entre c�maras
            if (Input.GetKeyDown(switchCameraKey))
            {
                mainCamera.enabled = !mainCamera.enabled;
                hoodCamera.enabled = !hoodCamera.enabled;
            }

            if (HasCrossedFinishLine())
            {
                FinishGame(); // Llama a la funci�n para finalizar el juego
            }
        }
    }


    bool HasCrossedFinishLine()
    {
        // Comprueba si el veh�culo ha cruzado la l�nea de meta
        if (finishLine != null)
        {
            Collider finishLineCollider = finishLine.GetComponent<Collider>();
            if (finishLineCollider != null && finishLineCollider.bounds.Contains(transform.position))
            {
                return true;
            }
            finishLineSound.Play();
        }
        return false;
    }

    void FinishGame()
    {
        // Agrega aqu� cualquier l�gica para finalizar el juego, como mostrar una pantalla de fin de juego o cargar otra escena.
        // Puedes mostrar un mensaje de victoria o derrota, reiniciar el nivel, cargar una nueva escena, etc.
        // Por ejemplo, puedes mostrar un mensaje de victoria en la consola:
        Debug.Log("�Juego terminado!");

        // Luego, establece el indicador de juego terminado en true para evitar que se siga ejecutando la l�gica del juego.
        gameFinished = true;

        // Redirige la c�mara hacia el mapa (asumiendo que tienes una referencia a la c�mara del mapa)
        mainCamera.enabled = false;
        finishCamera.enabled = true; // O habilita la c�mara del mapa seg�n tu configuraci�n

        
        gameOverText.enabled = true;

        // Luego, establece el indicador de juego terminado en true para evitar que se siga ejecutando la l�gica del juego.
        gameFinished = true;

        // Redirige la c�mara hacia el mapa (asumiendo que tienes una referencia a la c�mara del mapa)
        mainCamera.enabled = false;
        finishCamera.enabled = true; // O habilita la c�mara del mapa seg�n tu configuraci�n

        // Destruye el veh�culo
        motorSound.Stop();
    }


    // M�todo para consumir gasolina
    void ConsumeGas(float amount)
    {
        currentGas -= amount;
        if (currentGas < 0)
        {
            currentGas = 0;
        }
        UpdateEnergyBar();

        // Si te quedas sin gasolina, det�n el veh�culo
        if (currentGas == 0 && isMoving)
        {
            speed = 0;
        }
    }

    // M�todo para actualizar la barra de gasolina en la UI
    void UpdateEnergyBar()
    {
        energyBar.fillAmount = currentGas / maxGas;
    }

    // M�todo para manejar colisiones
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cylinder"))
        {
            gasSound.Play();
            // Detecta una colisi�n con un cilindro
            Debug.Log("Colisi�n con cilindro");
            CollectGasoline(10); // Gana 20 de gasolina
            Destroy(other.gameObject); // Destruye el cilindro
           
        }
        else
        {
            gasSound.Stop();
        }
    }

    // M�todo para ganar gasolina
    void CollectGasoline(float amount)
    {
        currentGas += amount;
        if (currentGas > maxGas)
        {
            currentGas = maxGas;
        }
        UpdateEnergyBar();
    }
}
