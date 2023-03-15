using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCollector : MonoBehaviour
{
    private int _coins = 0;

    [SerializeField] private CoinCollectionChecker[] _coinTrigger;

    public event UnityAction<int> IncreasedCoinsCount;

    private void OnEnable()
    {
        foreach (CoinCollectionChecker coinTrigger in _coinTrigger)
        {
            coinTrigger.CoinCollected += OnCoinCollected;
        }

    }

    private void OnDisable()
    {
        foreach (CoinCollectionChecker coinTrigger in _coinTrigger)
        {
            coinTrigger.CoinCollected += OnCoinCollected;
        }
    }

    private void OnCoinCollected()
    {
        _coins++;
        IncreasedCoinsCount?.Invoke(_coins);
    }
}
