using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondPicker : MonoBehaviour
{
    public TMP_Text diamondsText;

    private float diamonds = 0;


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Diamond")
        {
            diamonds++;
            diamondsText.text = diamonds.ToString();
            Destroy(coll.gameObject);

            //if (diamonds > 0)
            //{
            //    StartCoroutine(ImageAfterDelay(3f));


            //}
        }
    }
}
