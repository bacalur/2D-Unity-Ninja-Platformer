using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Coin")
        {
            Destroy(coll.gameObject);
        }
    }
}
