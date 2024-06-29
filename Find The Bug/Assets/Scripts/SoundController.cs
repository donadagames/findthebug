using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip regularCardClip;
    public AudioClip bugCardClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void PlayRegularCardSound()
    {
        PlaySound(regularCardClip);
    }

    public void PlayBugCardSound()
    {
        PlaySound(bugCardClip);
    }
}
