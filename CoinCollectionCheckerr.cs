using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCollectionChecker : MonoBehaviour
{
    private float _waitingTime = 10;
   
    public event UnityAction CoinCollected;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            CoinCollected?.Invoke();

            gameObject.SetActive(false);

            Invoke(nameof(ActivateCoin), _waitingTime);
        }   
    }

    private void ActivateCoin()
    {
        gameObject.SetActive(true);
    }
}
