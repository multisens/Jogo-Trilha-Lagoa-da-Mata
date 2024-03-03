using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PontuacaoCastanhas : MonoBehaviour
{
    public int coins = 0;
    public Text Coins_Text;

    public void UpdateCoins()
    {
        Coins_Text.text = "Castanhas: " + coins;
    }
}
