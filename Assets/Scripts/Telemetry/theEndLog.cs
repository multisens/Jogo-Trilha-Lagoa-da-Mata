using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class theEndLog : MonoBehaviour
{

    private static string logGameData = "Player,Questao,Resposta_Correta,Resposta_Player";

    public void LogData(string Log)
    {
        logGameData += "\n" + Log;
        Debug.Log(Log);
    }

    IEnumerator SendLog()
    {

        UnityWebRequest www = UnityWebRequest.Post("https://cursa.eic.cefet-rj.br/jogotrilha/log.php", logGameData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

        try
        {
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                throw new Exception("Erro ao enviar log: " + www.error);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SendLog());
    }
}
