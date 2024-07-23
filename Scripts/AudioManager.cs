using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public BgMusic[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
         if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playMusic("Bgm");
    }

    public void playMusic(string name)
    {
        BgMusic s = Array.Find(musicSounds, x=> x.name == name);

        if(s == null )
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void playSfx(string name)
    {
        BgMusic s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void MuteMusic()
    {
        if (musicSource != null)
        {
            musicSource.mute = !musicSource.mute;
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
