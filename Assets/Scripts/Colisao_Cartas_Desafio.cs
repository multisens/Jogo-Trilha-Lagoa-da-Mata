using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisao_Cartas_Desafio : MonoBehaviour
{
    public GameObject Carta_Desafio;
    private GameObject cartaAcaoInstanciada;

    public Transform Desafio_Transform;

    void OnTriggerEnter(Collider other)
    {
        cartaAcaoInstanciada = Instantiate(Carta_Desafio, Desafio_Transform.position, Desafio_Transform.rotation);

    }

    void OnTriggerExit(Collider other)
    {
        if (cartaAcaoInstanciada != null)
        {
            Destroy(cartaAcaoInstanciada);
        }
    }

}
