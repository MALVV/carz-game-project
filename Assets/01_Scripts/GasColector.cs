using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GasColector : MonoBehaviour
{
    public float gasAmount = 20f; // Cantidad de gasolina a recolectar
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            EnergyBarController bar = other.GetComponent<EnergyBarController>();
            if (bar != null)
            {
                //bar.AddGas(gasAmount); // Recolecta gasolina cuando el jugador entra en contacto
                Destroy(gameObject); // Destruye el recolector de gasolina
            }
            
        }
    }
}
