using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private static bool isMusicEnabled = true;
    private static float musicVolume = 1f;

    public static bool IsMusicEnabled
    {
        get { return isMusicEnabled; }
        set { isMusicEnabled = value; }
    }

    public static float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }
    }

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();


    }

    private void Start()
    {
        // Loading saved values from PlayerPrefs
        AudioManager.IsMusicEnabled = PlayerPrefs.GetInt("IsMusicEnabled", 1) == 1;
        AudioManager.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // Checking the saved music state
        if (AudioManager.IsMusicEnabled)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        // Setting the saved music volume value
        audioSource.volume = AudioManager.MusicVolume;
    }

    private void OnDestroy()
    {
        // Saving values to PlayerPrefs when the object is destroyed
        PlayerPrefs.SetInt("IsMusicEnabled", AudioManager.IsMusicEnabled ? 1 : 0);
        PlayerPrefs.SetFloat("MusicVolume", AudioManager.MusicVolume);
    }
}
