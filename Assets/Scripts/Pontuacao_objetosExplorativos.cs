using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public enum ObjetosEspeciais
{
    Any,
    FlorCarajas,
    Orquidea,
    Onca,
    arara,
    tartaruga,
    Castanha,
    Macaco,
    Capivara,
    Ninho


};


public class Pontuacao_objetosExplorativos : PontuacaoCastanhas
{
    public List_GameManager LGM;
    public GameObject ExplorativeObject;
    private GameObject PlacaInstanciada;

    public int ContadorInteracao = 0;
    private string pontos;
    /*private int Pontos = 1; */
    public ObjetosEspeciais OpcoesObjetos = new ObjetosEspeciais();
    [SerializeField] private Transform Pos_PlacaObjetoExplor;

    [SerializeField] private GameObject PlacaObjetoExplor;

    public string nomePlayer;
    private string dataFilePath;

    void Awake()
    {
        dataFilePath = Application.persistentDataPath + "/playerName.json";

    }    
    private void Start()
    {
        LGM = FindObjectOfType<List_GameManager>();
        if (LGM == null)
        {
            Debug.LogError("Objeto List_GameManager nao encontrado na cena.");
        }

    }   
    
    
    public void sendMessagePoint(string name, string points)
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


    private void OnTriggerEnter(Collider other)
    {
        if (ContadorInteracao != 0)
        {
            PlacaInstanciada = Instantiate(PlacaObjetoExplor, Pos_PlacaObjetoExplor.position, Pos_PlacaObjetoExplor.rotation);
        }
        else
        {
            switch (OpcoesObjetos)
            {
                case ObjetosEspeciais.FlorCarajas:
                    Debug.Log("FlorCarajas explorada!");
                    UpdateCoins(OpcoesObjetos.ToString(),70);
                    break;
                case ObjetosEspeciais.Orquidea:
                    UpdateCoins(OpcoesObjetos.ToString(),70);
                    break;
                case ObjetosEspeciais.Onca:
                    Debug.Log("onca explorada!");
                    UpdateCoins(OpcoesObjetos.ToString(),30);
                    break;
                case ObjetosEspeciais.arara:
                    Debug.Log("arara explorada!");
                    UpdateCoins(OpcoesObjetos.ToString(),60);
                    break;
                case ObjetosEspeciais.tartaruga:
                    Debug.Log("tartaruga explorada!");
                    UpdateCoins(OpcoesObjetos.ToString(),30);
                    break;
                case ObjetosEspeciais.Castanha:
                    Debug.Log("Castanha explorada!");
                    UpdateCoins(OpcoesObjetos.ToString(),80);
                    break;
                case ObjetosEspeciais.Macaco:
                    Debug.Log("Macaco explorada!");
                    UpdateCoins(OpcoesObjetos.ToString(),50);
                    break;
                case ObjetosEspeciais.Capivara:
                    Debug.Log("Capivara explorada!");
                    UpdateCoins(OpcoesObjetos.ToString(),10);
                    break;
                default:
                    Debug.Log("Objeto explorado: " + OpcoesObjetos);
                    break;
            }
            string no = GetPlayerName();
            Debug.Log(no);
            ContadorInteracao = 1;
            PlacaInstanciada = Instantiate(PlacaObjetoExplor, Pos_PlacaObjetoExplor.position, Pos_PlacaObjetoExplor.rotation);
        }
    }



    void OnTriggerExit(Collider other)
    {
        if (PlacaInstanciada != null)
        {
            Destroy(PlacaInstanciada);
        }
    }

    private void UpdateCoins(string objt, int amount)
    {
        nomePlayer = GetPlayerName();

            pontos = "Objeto Explorativo," + objt + "," + amount.ToString();
            sendMessagePoint(nomePlayer, pontos);

        GameObject[] xrObjects = GameObject.FindGameObjectsWithTag("canva");

        foreach (GameObject xrObject in xrObjects)
        {
            PontuacaoCastanhas pontuacaoScript = xrObject.GetComponent<PontuacaoCastanhas>();
            if (pontuacaoScript != null)
            {
                pontuacaoScript.UpdateCoins(amount);
            }
        }
    }

    public string GetPlayerName()
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

}
