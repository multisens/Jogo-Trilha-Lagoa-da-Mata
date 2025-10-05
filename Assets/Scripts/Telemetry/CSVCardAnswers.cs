// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using WebSocketSharp;

// public class CSVCardAnswers : SendData
// {
//     protected string name = "jogador"; //fazer um arquivo a parte para manter o nome dos jogadores e um para manter apenar o do jogador atual
//     protected string answers = "1000"; //pegar do outro arquivo dps
//     public GameObject wsC;
//     public void sendMessagePoint(string name, string answers)
//     { 

//         WS_Client.Message message = new WS_Client.Message
//         {
//             action = "cardanswer",
//             dataAnswer = new List<WS_Client.CardAnswer> // Lista de dados do jogador
//             {
//                 new WS_Client.CardAnswer { name = name, answers = answers }
//             }

//         };

//         string jsonData = JsonUtility.ToJson(message);
//         // wsclientescript.WebSocketInstance.Send(jsonData);
//         //
//         if(wsClient == null){
//             Debug.Log("aaaaaaaaa");
//         }
//         wsC.GetComponent<WS_Client>().WebSocketInstance.Send(jsonData);
//         Debug.Log("Mensagem enviada para gerar csv dos pontos: " + jsonData);
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         sendMessagePoint(name, answers);
//     }
// }
