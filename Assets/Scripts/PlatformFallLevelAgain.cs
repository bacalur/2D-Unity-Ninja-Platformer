using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformFallLevelAgain : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
