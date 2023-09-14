using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisao_Cartas_Acao : MonoBehaviour
{
    public GameObject Carta_Acao;

    public Transform Acao_Transform;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(Carta_Acao, Acao_Transform.position, Acao_Transform.rotation);

    }

}
