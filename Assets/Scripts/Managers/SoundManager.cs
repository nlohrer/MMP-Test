using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource SoundSource;

    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
    }

    public void PlayClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (audioClip == null)
        {
            return;
        }
        AudioSource audioSource = Instantiate(SoundSource, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public AudioSource PlayLoopingClip(AudioClip audioClip, Transform parent, float volume)
    {
        AudioSource audioSource = Instantiate(SoundSource, parent, worldPositionStays: false);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();

        return audioSource;
    }
}
