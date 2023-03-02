using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountWriter : MonoBehaviour
{
    [SerializeField]  private Text coinText;
    private int coin = 0;

    private void Start()
    {
        CoinTrigger.increaseOfCoins += WriteCount;
    }

    private void WriteCount()
    {
        coin++;
        coinText.text = $"{coin}";
    }
}
