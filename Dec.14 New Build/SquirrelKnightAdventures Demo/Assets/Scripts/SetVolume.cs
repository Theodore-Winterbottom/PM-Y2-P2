using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    public List<AudioSource> audioSources;


    private float musicVolume = 1f;

    public void Update()
    {
        audioSource.volume = musicVolume;
        audioSource2.volume = musicVolume;
        audioSource3.volume = musicVolume;
    }

    public void Sound(float volume)
    {
        musicVolume= volume;
    }

    public void Mute(bool muted)
    {
        if(muted)
        {
            musicVolume= 0f;
        }
        else
        {
            musicVolume= 1f;
        }
    }

    public void DropDown(int value)
    {

        if (value == 0)
        {
            audioSource.Play();
            audioSource2.Stop();
            audioSource3.Stop();
        }

        if (value == 1)
        {
            audioSource2.Play();
            audioSource.Stop();
            audioSource3.Stop();
        }

        if (value == 2)
        {
            audioSource3.Play();
            audioSource.Stop();
            audioSource2.Stop();
        }
    }
}
