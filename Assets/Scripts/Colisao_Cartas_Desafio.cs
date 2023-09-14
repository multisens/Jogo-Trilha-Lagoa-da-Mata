using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisao_Cartas_Desafio : MonoBehaviour
{
    public GameObject Carta_Desafio;

    public Transform Desafio_Transform;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(Carta_Desafio, Desafio_Transform.position, Desafio_Transform.rotation);

    }

}
