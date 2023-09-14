using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class List_GameManager : MonoBehaviour
{

    private string logGameData;

    void Awake()
    {
        logGameData = "Player,Questao,Resposta_Correta,Resposta_Player";
    }

    public void LogData(string log)
    {
        logGameData += "\n" + log;
        Debug.Log(log);
    }

}