using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alt_Caminho : MonoBehaviour
{

    public GameObject Arbusto1;
    public GameObject Arbusto2;
    
    public float speed;
    public Transform ponto1;
    public Transform ponto2;

    public GameObject sumir1;
    public GameObject sumir2;


    private Vector3 ponto_Abrir1;
    private Vector3 ponto_Abrir2;

    void Start()
    {
        ponto_Abrir1 = ponto1.transform.position;
        ponto_Abrir2 = ponto2.transform.position;
    }

    void OnTriggerStay(Collider other)
    {

        MoveBush();

    }


    void MoveBush()
    {
        Arbusto1.transform.position = Vector3.MoveTowards(Arbusto1.transform.position, ponto_Abrir1, speed * Time.deltaTime);
        Arbusto2.transform.position = Vector3.MoveTowards(Arbusto2.transform.position, ponto_Abrir2, speed * Time.deltaTime);
        Destroy(sumir1);
        Destroy(sumir2);
    }

}
