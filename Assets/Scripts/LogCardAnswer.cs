using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static List_GameManager;

public class LogCardAnswer : MonoBehaviour
{
    public GameObject cardObject;
    public LogCardData cardData;
    public List_GameManager LGM;
   
    private MeshRenderer cardRenderer;
    private Color originalColor;

    [Header("Colors")]
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public float delayTime = 3f;
    
    private void Start()
    {
        
        LGM = FindObjectOfType<List_GameManager>();
        if (LGM == null)
        {
            Debug.LogError("Objeto List_GameManager nao encontrado na cena.");
        }

        cardRenderer = cardObject.GetComponent<MeshRenderer>();
        if (cardRenderer == null)
        {
            cardRenderer = cardObject.AddComponent<MeshRenderer>();
        }

        originalColor = cardRenderer.material.color;
    }

    private void OnTriggerExit(Collider other)
    {
        cardData = cardObject.GetComponent<LogCardData>();
        string log = "Player1," + cardData.cardID + "," + cardData.correctAnswer + "," + gameObject.name;
        LGM.LogData(log);


        if (cardData.correctAnswer == gameObject.name)
        {
            UpdateCoins(1);
            StartCoroutine(ChangeColorAndDisappear(cardRenderer, correctColor, delayTime));
        }
        else
        {
            StartCoroutine(ChangeColorAndDisappear(cardRenderer, incorrectColor, delayTime));
        }

        Destroy(cardObject, delayTime);
    }

    private IEnumerator ChangeColorAndDisappear(MeshRenderer renderer, Color targetColor, float delay)
    {
        renderer.material.color = targetColor;
        yield return new WaitForSeconds(delay);
        renderer.material.color = originalColor;
    }
  
        private void UpdateCoins(int amount)
    {
        GameObject[] xrObjects = GameObject.FindGameObjectsWithTag("xr");

        foreach (GameObject xrObject in xrObjects)
        {
            PontuacaoCastanhas pontuacaoScript = xrObject.GetComponent<PontuacaoCastanhas>();
            if (pontuacaoScript != null)
            {
                pontuacaoScript.coins += amount;
                Debug.Log(pontuacaoScript.coins);
                pontuacaoScript.UpdateCoins();
            }
        }
    }

}
