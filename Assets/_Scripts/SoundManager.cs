using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip LoseSound, HitSound, BashtoySound, StartSound;

    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        PlaySound(StartSound, 0.5f, 1.0f);
    }

    public void PlaySound(AudioClip clip, float vol, float pitch)
    {
        audioSource.clip = clip;
        audioSource.volume = vol;
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
