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

    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void PlayRegularCardSound()
    {
        audioSource.PlayOneShot(regularCardClip);
    }

    public void PlayBugCardSound()
    {
        audioSource.PlayOneShot(bugCardClip);
    }
}
