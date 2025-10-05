using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static List_GameManager;
using WebSocketSharp;

public class LogCardAnswer : SendData
{
    public GameObject cardObject;
    public LogCardData cardData;
    //public List_GameManager LGM;
   
    private MeshRenderer cardRenderer;
    private Color originalColor;

    [Header("Colors")]
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public float delayTime = 3f;
    private int Pontos = 70; 
    private string pontos;
    private string name;
    public GameObject RESP1;
    public GameObject RESP2;
    private string dataFilePath;

    public void sendMessagePoint(string name, string answers)
    { 
        WS_Client.Message message = new WS_Client.Message
        {
            action = "cardanswer",
            dataAnswer = new List<WS_Client.CardAnswer> // Lista de dados do jogador
            {
                new WS_Client.CardAnswer { name = name, answers = answers }
            }

        };

        string jsonData = JsonUtility.ToJson(message);

 
        WS_Client.instance.WebSocketInstance.Send(jsonData);
        Debug.Log("Mensagem enviada para gerar csv das respostas: " + jsonData);
    }

 private void SavePlayerData(string name)
    {
        PlayerDataInfo playerData = new PlayerDataInfo { name = name };

        

        // using (StreamWriter writer = new StreamWriter(dataFilePath))
        // {
        //     writer.Write(json); 
        // }
        List<PlayerDataInfo> newData = new List<PlayerDataInfo> { playerData };
        PlayerDataList dataList = new PlayerDataList { players = newData };

        string fileName = "playerName.json";
        string path = Application.persistentDataPath + "/" + fileName;

                // Se o arquivo já existe, leia os dados
        //List<PlayerDataInfo> existingData = LoadPlayerData();

        // Adiciona o novo jogador
        //existingData.Add(playerData);



        string json = JsonUtility.ToJson(dataList, true);

        string content = json;

        File.WriteAllText(path, content); 

        Debug.Log($"Dados do jogador salvos em: {dataFilePath}");
        Debug.Log($"Conteúdo salvo: {json}");
    }

    void Awake()
    {
        dataFilePath = Application.persistentDataPath + "/playerName.json";
    }    
    
    private void Start()
    {
        cardRenderer = cardObject.GetComponent<MeshRenderer>();
        if (cardRenderer == null)
        {
            cardRenderer = cardObject.AddComponent<MeshRenderer>();
        }
        originalColor = cardRenderer.material.color;
    }

    public string GetPlayerName()
        {
            string path = Application.persistentDataPath + "/playerName.json";

            // Verificar se o arquivo existe
            if (!File.Exists(path))
            {
                Debug.LogWarning("Arquivo playerName.json não encontrado.");
                return "empty";
            }

            List<PlayerDataInfo> playerData = LoadPlayerData();

            if (playerData == null || playerData.Count == 0)
            {
                Debug.LogWarning("Nenhum dado encontrado no arquivo playerName.json.");
                return "empty";
            }
            
            // Concatenar os nomes em uma string
            string names = string.Join(", ", playerData.ConvertAll(player => player.name));

            if(!string.IsNullOrEmpty(names)){
                return names;
            }else{
                return "empty";
            }
        }


    public List<PlayerDataInfo> LoadPlayerData()
    {
            string json = File.ReadAllText(dataFilePath);
            PlayerDataList dataList = JsonUtility.FromJson<PlayerDataList>(json);
            return dataList.players;

    }
    public void sendMessagePoints(string name, string points)
    { 
        WS_Client.Message message = new WS_Client.Message
        {
            action = "csvpoints",
            data = new List<WS_Client.PlayerData> // Lista de dados do jogador
            {
                new WS_Client.PlayerData { name = name, points = points }
            }

        };

        string jsonData = JsonUtility.ToJson(message);

 
        WS_Client.instance.WebSocketInstance.Send(jsonData);
        Debug.Log("Mensagem enviada para gerar csv dos pontos: " + jsonData);
    }

    private void OnTriggerExit(Collider other)
    {
        cardData = cardObject.GetComponent<LogCardData>();
        string log =  cardData.cardID + "," + cardData.correctAnswer + "," + gameObject.name;
        //LGM.LogData(log);
            name = GetPlayerName();
            sendMessagePoint(name,log);
        if (cardData.correctAnswer == gameObject.name)
        {
            UpdateCoins(cardData.cardID,Pontos);
            StartCoroutine(ChangeColorAndDisappear(cardRenderer, correctColor, delayTime));
        }
        else
        {
            UpdateCoins(cardData.cardID,0);
            StartCoroutine(ChangeColorAndDisappear(cardRenderer, incorrectColor, delayTime));
        }
        Destroy(RESP1.GetComponent<BoxCollider>());
        Destroy(RESP2.GetComponent<BoxCollider>());
        Destroy(cardObject, delayTime);
    }



    private IEnumerator ChangeColorAndDisappear(MeshRenderer renderer, Color targetColor, float delay)
    {
        renderer.material.color = targetColor;
        yield return new WaitForSeconds(delay);
        renderer.material.color = originalColor;
    }
  

        private void UpdateCoins(string objt, int amount)
    {

        pontos = "Carta Desafio," + objt + "," + amount.ToString();
        name = GetPlayerName();
        sendMessagePoints(name, pontos);

        GameObject[] xrObjects = GameObject.FindGameObjectsWithTag("canva");

        foreach (GameObject xrObject in xrObjects)
        {
            PontuacaoCastanhas pontuacaoScript = xrObject.GetComponent<PontuacaoCastanhas>();
            if (pontuacaoScript != null)
            {
                pontuacaoScript.UpdateCoins(Pontos);
            }
        }
    }


    [System.Serializable]
    public class PlayerDataList
    {
        public List<PlayerDataInfo> players;
    }

    [System.Serializable]
    public class PlayerDataInfo
    {
        public string name;
    }


}
