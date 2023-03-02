using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinTrigger : MonoBehaviour
{
    public static event UnityAction increaseOfCoins;
    private float _waitingTime = 10;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            increaseOfCoins?.Invoke();

            gameObject.SetActive(false);

            Invoke(nameof(ActivateCoin), _waitingTime);
        }   
    }

    private void ActivateCoin()
    {
        gameObject.SetActive(true);
    }
}
