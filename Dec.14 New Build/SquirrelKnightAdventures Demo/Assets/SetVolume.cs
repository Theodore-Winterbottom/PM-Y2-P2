using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioSource audioSource;

    private float musicVolume = 1f;

    public void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void Sound(float volume)
    {
        musicVolume= volume;
    }
}
