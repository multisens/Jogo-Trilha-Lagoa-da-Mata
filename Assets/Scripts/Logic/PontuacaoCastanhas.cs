using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PontuacaoCastanhas : MonoBehaviour
{
    public int coins = 0;
    public Text Coins_Text;


    public void UpdateCoins(int amount)
    {   

        // Atualiza a quantidade de moedas
        coins += amount;

        // Atualiza a UI com o novo valor de moedas
        if (Coins_Text != null)
        {
            Coins_Text.text = "Castanhas: " + coins;
        }

        // Exibe o valor no console para debug
        Debug.Log("Moedas Atualizadas: " + coins);
    }
    

    protected void FlorPontos(){
        int n = 0;
        n = n + 1;
        if(n == 3){
            UpdateCoins(100);
        }
    }
}
