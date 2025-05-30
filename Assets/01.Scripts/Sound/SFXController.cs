using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXController : MonoBehaviour
{
    public AudioMixer sfxAudioMixer;     // MainAudioMixer
    public Slider sfxSlider;
    public Toggle sfxMuteToggle;

    private const string sfxParam = "SFXVolume";
    private float sfxPreviousVolume = 1f;

    void Start()
    {
        PlayerPrefs.SetFloat(sfxParam, 1f); // 강제 초기화
        PlayerPrefs.Save();
        if (sfxSlider != null)
        {
            float savedSfx = PlayerPrefs.GetFloat(sfxParam, 1f);
            sfxSlider.value = savedSfx;
            sfxSetVolume(savedSfx);
            sfxSlider.onValueChanged.AddListener(sfxSetVolume);
        }

        if (sfxMuteToggle != null)
        {
            sfxMuteToggle.onValueChanged.AddListener(sfxSetMute);
        }
    }

    public void sfxSetVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        sfxAudioMixer.SetFloat(sfxParam, dB);
        sfxPreviousVolume = volume;
        PlayerPrefs.SetFloat(sfxParam, volume);
        if (sfxMuteToggle != null && sfxMuteToggle.isOn)
        {
            sfxMuteToggle.isOn = false;
        }
    }

    public void sfxSetMute(bool isMuted)
    {
        if (isMuted)
        {
            sfxAudioMixer.SetFloat(sfxParam, -80f);
        }
        else
        {
            sfxSetVolume(sfxSlider.value);
        }
    }
}
