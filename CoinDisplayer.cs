using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplayer : MonoBehaviour
{
    [SerializeField]  private Text coinText;
    [SerializeField] private CoinCollector coinCollector;
  

    private void OnEnable()
    {
        coinCollector.IncreasedCoinsCount += DrawCoins;          
    }

    private void OnDisable()
    {
        coinCollector.IncreasedCoinsCount -= DrawCoins;
    }

    private void DrawCoins(int coinsCount)
    {
        coinText.text = $"{coinsCount}";
    }
}
