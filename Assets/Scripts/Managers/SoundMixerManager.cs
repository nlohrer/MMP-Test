using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    public AudioMixer AudioMixer;

    public void SetMasterVolume(float level)
    {
        AudioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetEffectsVolume(float level)
    {
        AudioMixer.SetFloat("EffectsVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }
}
