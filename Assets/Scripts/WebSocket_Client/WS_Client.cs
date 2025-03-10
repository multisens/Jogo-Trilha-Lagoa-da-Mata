using System.IO;
using WebSocketSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class WS_Client : MonoBehaviour
{
    private WebSocket ws;
    public int sceneIndexToLoad = 1;
    public static WS_Client instance;

    private List<string> playerNames = new List<string>();

    public GameObject playerNamesPrefab; // Prefab do objeto TextMeshPro para exibir os nomes dos jogadores
    public Transform namesParent;

    public WebSocket WebSocketInstance
    {
        get { return ws; }
    }

    public static WS_Client Instance
    {
        get { return instance; }
    }

    private string dataFilePath;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        dataFilePath = Application.persistentDataPath + "/playerName.json";

        ws = new WebSocket("ws://10.101.0.154:7760");
        // ws = new WebSocket("ws://192.168.1.3:7760");
        ws.OnMessage += (sender, e) =>
        {
            HandleMessage(e.Data);
            Debug.Log("Mensagem recebida de: " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
        ws.ConnectAsync();
                    

    }

    public void UpdatePlayerList(List<string> playerNames)
    {
        // Limpa os textos de jogador existentes
        ClearPlayerTexts();

        // Cria um objeto TextMeshPro para cada nome de jogador na lista e adiciona à lista de objetos
        foreach (string playerName in playerNames)
        {
            GameObject newText = Instantiate(playerNamesPrefab, namesParent);
            newText.GetComponent<TextMeshProUGUI>().text = playerName;
        }
    }

    private void ClearPlayerTexts()
    {
        // Destroi todos os objetos TextMeshPro existentes na lista e limpa a lista
        foreach (Transform child in namesParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void HandleMessage(string data)
    {
        Message message = JsonUtility.FromJson<Message>(data);
        if (message.action == "start_scene")
        {
            Debug.Log("loadscene now");
            LoadScene();
        }
        else if (message.action == "connect")
        {
            playerNames.Add(message.name);
            UpdatePlayerList(playerNames);
        }
    }

    public void LoadScene()
    {
        Debug.Log("Scene index to load: " + sceneIndexToLoad);
        SceneManager.LoadScene(sceneIndexToLoad);
    }

    private void OnDestroy()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }
    }

    public string GetPlayerNames()
        {
            string path = Application.persistentDataPath + "/playerName.json";
            
            List<PlayerDataInfo> playerData = LoadPlayerData();

            // Concatenar os nomes em uma string
            string names = string.Join(", ", playerData.ConvertAll(player => player.name));

            return names;
        }


    public List<PlayerDataInfo> LoadPlayerData()
    {
            string json = File.ReadAllText(dataFilePath);
            PlayerDataList dataList = JsonUtility.FromJson<PlayerDataList>(json);
            return dataList.players;

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
    [System.Serializable]
    public class PlayerData
    {
        public string name;   // Nome do jogador
        public string points;    // Pontos do jogador
    }
   [System.Serializable]
    public class CardAnswer
    {
        public string name;   // Nome do jogador
        public string answers;    // Pontos do jogador
    }


    [System.Serializable]
    public class Message
    {
        public string action;
        public string name;
        public string room;
        public List<PlayerData> data; // Lista de dados do jogador
        public List<CardAnswer> dataAnswer; // Lista de dados do jogador

    }

}
