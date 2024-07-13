using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    public AudioMixer AudioMixer;

    public Slider masterVolumeSlider;
    public Slider effectsVolumeSlider;
    public Slider musicVolumeSlider;

    private void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public void SetMasterVolume(float level)
    {
        AudioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    public void SetEffectsVolume(float level)
    {
        AudioMixer.SetFloat("EffectsVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("EffectsVolume", level);
    }

    public void SetMusicVolume(float level)
    {
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", level );
    }
}
