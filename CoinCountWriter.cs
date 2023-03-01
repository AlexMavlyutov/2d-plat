using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountWriter : MonoBehaviour
{
    public Text coinText;
    public static int coin;

    private void FixedUpdate()
    {
        coinText.text = $"{coin}";
    }
}
