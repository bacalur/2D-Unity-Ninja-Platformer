using UnityEngine;
using UnityEngine.SceneManagement;

public class DiamondCollision : MonoBehaviour
{
    public GameObject victoryImage;
    public float victoryDuration = 3f;
    public string nextSceneName = "SecondScene";

    private bool isVictoryDisplayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Diamond") && !isVictoryDisplayed)
        {
            isVictoryDisplayed = true;
            victoryImage.SetActive(true);

            Invoke("LoadNextScene", victoryDuration);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
