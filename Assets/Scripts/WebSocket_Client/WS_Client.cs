/*using WebSocketSharp;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class WS_Client : MonoBehaviour
{

    public GameObject CanvaPlayerName;
    public GameObject CanvaInic;
    public GameObject Canvacontroll;
    private List<string> playersInRoom = new List<string>();

    public GameObject playerTextPrefab;
    public Transform playerTextParent;

    public void WaitRoom()
    {

        Debug.Log("RoomCreated: ");
        CanvaPlayerName.SetActive(true);
        CanvaInic.SetActive(false);
        Canvacontroll.SetActive(false);
    }


    public void Update()
    {

        UpdatePlayerNamesUI();
    }


    private void UpdatePlayerNamesUI()
    {
        foreach (Transform child in playerTextParent)
        {
            Destroy(child.gameObject);
        }

        foreach (string playerName in playersInRoom)
        {
            GameObject playerTextInstance = Instantiate(playerTextPrefab, playerTextParent);

            TextMeshProUGUI textMesh = playerTextInstance.GetComponent<TextMeshProUGUI>();
            textMesh.text = playerName;
        }
    }


*/



using WebSocketSharp;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WS_Client : MonoBehaviour
{
    private WebSocket ws;
    public int sceneIndexToLoad = 1;
    static WS_Client instance;


    public WebSocket WebSocketInstance
    {
        get { return ws; }
    }

    public static WS_Client Instance
    {
        get { return instance; }
    }


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        ws = new WebSocket("ws://192.168.0.213:7760");
        ws.OnMessage += (sender, e) =>
        {
            HandleMessage(e.Data);
            Debug.Log("Mensagem recebida de: " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
        ws.Connect();
    }

    private void HandleMessage(string data)
    {
        Message message = JsonUtility.FromJson<Message>(data);
        if (message.action == "start_scene")
        {
            Debug.Log("loadscene now");
            LoadScene();
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

[System.Serializable]
public class Message
{
    public string action;
    public string name; 
    public string room;
}

}




