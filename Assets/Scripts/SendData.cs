
using System.IO;
using WebSocketSharp;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
public class SendData : MonoBehaviour
{

    public WS_Client wsClient;
    public TMP_InputField inputTextName;
    public TMP_InputField inputTextRoom;
    public GameObject canvasActive;
    public GameObject canvasInactive;
    public GameObject wsGameObject;

    public Button startButton;
    public TextMeshProUGUI waitMessageText;

    private bool allPlayersReady = false;



    void Start()
    {
        if (wsGameObject == null)
        {
            Debug.LogWarning("GameObject do WS não foi acessado");
            return;
        }

        wsClient = wsGameObject.GetComponent<WS_Client>();

        if (wsClient == null)
        {
            Debug.LogWarning("O Componente do WS não foi encontrado no GameObject");
        }

        startButton.onClick.AddListener(OnStartScene);

    }

    public void OnStartScene()
    {
        string name = inputTextName.text;
        string room = 1.ToString();

        Debug.Log("Name: " + name);
        Debug.Log("Room Number: " + room);

        WS_Client.Message message = new WS_Client.Message
        {
            action = "start",
            name = name,
            room = room
        };

        string jsonData = JsonUtility.ToJson(message);
        wsClient.WebSocketInstance.Send(jsonData);
        Debug.Log("Mensagem enviada para mudar de cena: " + jsonData);

        // Desativa o botão de iniciar e mostra a mensagem de espera
        startButton.interactable = false;
        waitMessageText.gameObject.SetActive(true);

    }

    private void SavePlayerData(string name)
    {
        PlayerDataInfo playerData = new PlayerDataInfo { name = name };

        List<PlayerDataInfo> newData = new List<PlayerDataInfo> { playerData };
        PlayerDataList dataList = new PlayerDataList { players = newData };

        string fileName = "playerName.json";
        string path = Application.persistentDataPath + "/" + fileName;

        string json = JsonUtility.ToJson(dataList, true);

        string content = json;

        File.WriteAllText(path, content);

        Debug.Log($"Dados do jogador salvos em: {path}");
        Debug.Log($"Conteúdo salvo: {json}");
    }


    public void OnConnect()
    {
        string name = inputTextName.text;
        string room = 1.ToString();

        Debug.Log("Name: " + name);
        Debug.Log("Room Number: " + room);
        // Salvar nome e sala no arquivo JSON
        SavePlayerData(name);
        WS_Client.Message message = new WS_Client.Message
        {
            action = "connect",
            name = name,
            room = room
        };

        string jsonData = JsonUtility.ToJson(message);
        wsClient.WebSocketInstance.Send(jsonData);
        Debug.Log("Mensagem enviada para mudar de cena: " + jsonData);
    }

    public void CreateRoom()
    {
        string name = inputTextName.text;
        string room = 1.ToString();

        Debug.Log("Name: " + name);
        Debug.Log("Room Number: " + room);

        WS_Client.Message message = new WS_Client.Message
        {
            action = "new_room",
            name = name,
            room = room
        };

        string jsonData = JsonUtility.ToJson(message);
        wsClient.WebSocketInstance.Send(jsonData);
        Debug.Log("Mensagem enviada para mudar de cena: " + jsonData);
    }

    public void ChangeCanvasToCreated()
    {
        canvasActive.SetActive(true);
        canvasInactive.SetActive(false);
    }


    public void exitGame()
    {
        Application.Quit();
    }


    public void AllPlayersReady()
    {
        allPlayersReady = true;

        if (allPlayersReady)
        {
            OnStartScene();
        }
        else
        {
            waitMessageText.gameObject.SetActive(true);
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

    [System.Serializable]
    public class Message
    {
        public string action;
        public string name; // Adicionei essa propriedade para representar o nome do jogador, como visto em algumas mensagens.
        public string room; // Adicionei essa propriedade para representar o número da sala, como visto em algumas mensagens.
    }

}
