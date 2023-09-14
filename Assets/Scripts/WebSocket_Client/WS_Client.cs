using WebSocketSharp;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WS_Client : MonoBehaviour
{
    static WS_Client instance; // Instancia o singleton
    private WebSocket ws;
    public int scene_int;
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
        DontDestroyOnLoad(this.gameObject); // Mantem o objeto durante as trocas de cena

        ws = new WebSocket("ws://localhost:8080");
        ws.OnMessage += (sender, e) =>
        {
            HandleMessage(e.Data); // Chama a função para tratar a mensagem
            Debug.Log("Mensagem recebida de: " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
        ws.Connect();

    }

    private void HandleMessage(string data)
    {
        // Analisa a mensagem do JSON
        Message message = JsonUtility.FromJson<Message>(data);

        if (message.action == "start_scene")
        {
            Debug.Log("loadscene now");
            LoadA();
        }
    }

    public void LoadA()
    {
        Debug.Log("sceneName to load: " + scene_int);
        SceneManager.LoadScene(scene_int);
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
    }
}
