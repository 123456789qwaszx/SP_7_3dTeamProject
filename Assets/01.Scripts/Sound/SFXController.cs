using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXController : MonoBehaviour
{
    public AudioMixer audioMixer;     // MainAudioMixer
    public Slider sfxSlider;
    public Toggle sfxMuteToggle;

    private const string param = "SFXVolume";
    private float previousVolume = 1f;

    void Start()
    {
        float saved = PlayerPrefs.GetFloat(param, 1f);
        sfxSlider.value = saved;
        SetVolume(saved);

        sfxSlider.onValueChanged.AddListener(SetVolume);
        sfxMuteToggle.onValueChanged.AddListener(SetMute);
    }

    public void SetVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        audioMixer.SetFloat(param, dB);
        previousVolume = volume;
        PlayerPrefs.SetFloat(param, volume);
    }

    public void SetMute(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.SetFloat(param, -80f);
        }
        else
        {
            SetVolume(sfxSlider.value);
        }
    }
}
