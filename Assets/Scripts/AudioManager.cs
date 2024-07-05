using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;    
    public AudioSource musicSource;
    public AudioSource audioSource;

    public AudioSource[] sfxSource;
    public AudioSource[] uiSource;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = musicSource;
        sfxSource = new AudioSource[8]; //2 per player?
        for (int i = 0; i < sfxSource.Length; i++)
        {
            sfxSource[i] = gameObject.AddComponent<AudioSource>();
        }
        uiSource = new AudioSource[2];
        for (int i = 0; i < uiSource.Length; i++)
        {
            uiSource[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = GameManager.Instance.options.MusicVolume;
    }
    // Update is called once per frame
    void Update()
    {
        //si hay algún bug de volumen de audio no actualizándose, actualizar los volúmenes aquí
    }

    public void PlayMusic(AudioClip clip = null)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
        }
        musicSource.volume = GameManager.Instance.options.MusicVolume;
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.volume = GameManager.Instance.options.MusicVolume;
        musicSource.UnPause();
    }

    public void PlayUISound(AudioClip clip)
    {
        for (int i = 0; i < uiSource.Length; i++)
        {
            if (!uiSource[i].isPlaying)
            {
                uiSource[i].clip = clip;
                uiSource[i].volume = GameManager.Instance.options.SfxVolume;
                uiSource[i].Play();
                break;
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        for (int i = 0; i < sfxSource.Length; i++)
        {
            if (!sfxSource[i].isPlaying)
            {
                sfxSource[i].clip = clip;
                sfxSource[i].volume = GameManager.Instance.options.SfxVolume;
                sfxSource[i].Play();
                break;
            }
        }
    }
}
