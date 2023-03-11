using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCollectionChecker : MonoBehaviour
{
    public event UnityAction IncreaseOfCoins;

    private float _waitingTime = 10;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            IncreaseOfCoins?.Invoke();

            gameObject.SetActive(false);

            Invoke(nameof(ActivateCoin), _waitingTime);
        }   
    }

    private void ActivateCoin()
    {
        gameObject.SetActive(true);
    }
}
