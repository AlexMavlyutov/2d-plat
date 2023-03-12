using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCoin : MonoBehaviour
{
    [SerializeField]  private Text coinText;
    [SerializeField] private CoinCollectionChecker[]  _coinTrigger ;
    
    private int _coin = 0;


    private void OnEnable()
    {
        foreach (CoinCollectionChecker coinTrigger in _coinTrigger)
        {
            coinTrigger.IncreaseOfCoins += OnIncreaseOfCOins;
        }            
    }

    private void OnDisable()
    {
        foreach (CoinCollectionChecker coinTrigger in _coinTrigger)
        {
            coinTrigger.IncreaseOfCoins += OnIncreaseOfCOins;
        }
    }

    private void OnIncreaseOfCOins()
    {
        _coin++;
        coinText.text = $"{_coin}";
    }
}
