using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    private float _waitingTime = 10;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinCountWriter.coin++;
            
            gameObject.SetActive(false);

            Invoke(nameof(ActivateCoin), _waitingTime);
        }   
    }

    private void ActivateCoin()
    {
        gameObject.SetActive(true);
    }
}
