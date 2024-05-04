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
        // ��������� ����������� �������� �� PlayerPrefs
        AudioManager.IsMusicEnabled = PlayerPrefs.GetInt("IsMusicEnabled", 1) == 1;
        AudioManager.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // ��������� ����������� ��������� ������
        if (AudioManager.IsMusicEnabled)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        // ������������� ����������� �������� ��������� ������
        audioSource.volume = AudioManager.MusicVolume;
    }

    private void OnDestroy()
    {
        // ��������� �������� � PlayerPrefs ��� ����������� �������
        PlayerPrefs.SetInt("IsMusicEnabled", AudioManager.IsMusicEnabled ? 1 : 0);
        PlayerPrefs.SetFloat("MusicVolume", AudioManager.MusicVolume);
    }
}
