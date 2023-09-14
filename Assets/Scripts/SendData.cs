using WebSocketSharp;
using UnityEngine;
using UnityEngine.UI;

public class SendData : MonoBehaviour
{
    public GameObject wsGameObject;
    WS_Client wsClient;
    public InputField Input_Text_Name;
    public InputField Input_Text_N_Sala;

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
    }

    public void OnClickSend()
    {
        string name = Input_Text_Name.text;
        string room = Input_Text_N_Sala.text;

        Debug.Log("Name: " + name);
        Debug.Log("Room Number: " + room);

        Message message = new Message
        {
            action = "start_scene",
            name = name,
            room = room
        };

        string jsonData = JsonUtility.ToJson(message);
        wsClient.WebSocketInstance.Send(jsonData);
        Debug.Log("Mensagem enviada para mudar de cena: " + jsonData);
    }

    [System.Serializable]
    public class Message
    {
        public string action;
        public string name;
        public string room;
    }
}
