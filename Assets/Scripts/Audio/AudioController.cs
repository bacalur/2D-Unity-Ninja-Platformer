using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Sprite audioOn;
    public Sprite audioOff;
    public GameObject buttonAudio;

    public Slider slider;

    public AudioClip clip;
    public AudioSource audioSource;

    private const string MusicEnabledKey = "MusicEnabled";
    private const string MusicVolumeKey = "MusicVolume";

    private bool isMusicEnabled;
    private float musicVolume;

    void Start()
    {
        // Загружаем сохраненные значения из PlayerPrefs
        isMusicEnabled = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1;
        musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);

        // Проверяем сохраненное состояние музыки
        if (isMusicEnabled)
        {
            audioSource.Play();
            buttonAudio.GetComponent<Image>().sprite = audioOn;
        }
        else
        {
            audioSource.Stop();
            buttonAudio.GetComponent<Image>().sprite = audioOff;
        }

        // Устанавливаем сохраненное значение громкости
        audioSource.volume = musicVolume;

        // Устанавливаем значение слайдера громкости
        slider.value = musicVolume;
    }

    void Update()
    {
        audioSource.volume = slider.value;
    }

    public void OnOffAudio()
    {
        isMusicEnabled = !isMusicEnabled;

        if (isMusicEnabled)
        {
            audioSource.Play();
            buttonAudio.GetComponent<Image>().sprite = audioOn;
        }
        else
        {
            audioSource.Stop();
            buttonAudio.GetComponent<Image>().sprite = audioOff;
        }

        // Сохраняем состояние музыки в PlayerPrefs
        PlayerPrefs.SetInt(MusicEnabledKey, isMusicEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(clip);
    }
}
