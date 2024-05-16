using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Right mouse button - pick up item
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("The ninja took the item");
        }
    }
}
