using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVpoint : SendData
{

    protected string name = "jogador"; //fazer um arquivo a parte para manter o nome dos jogadores e um para manter apenar o do jogador atual
    protected string points = "1000"; //pegar do outro arquivo dps


    public void sendMessagePoint(string name, string points)
    {


        Debug.Log("Name: " + name);
        Debug.Log("Points: " + points);

        WS_Client.Message message = new WS_Client.Message
        {
            action = "csvpoints",
            data = new List<WS_Client.PlayerData> // Lista de dados do jogador
            {
                new WS_Client.PlayerData { name = name, points = points }
            }

        };

        string jsonData = JsonUtility.ToJson(message);
        wsClient.WebSocketInstance.Send(jsonData);
        Debug.Log("Mensagem enviada para gerar csv dos pontos: " + jsonData);
    }

    private void OnTriggerEnter(Collider other)
    {
        sendMessagePoint(name, points);
    }


}
