using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    public TMP_Text coinsText;

    private float coins = 0;

    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Coin")
        {
            coins++;
            coinsText.text = coins.ToString();
            Destroy(coll.gameObject);
        }
    }
}
