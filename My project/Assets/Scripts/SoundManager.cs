using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    laser,
    click,
    game_over,
    jump_sound,
    crash,
    fall,
    star
}


[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    public static SoundManager Instance { get; private set; }

 
    private AudioSource oneShotSource; 
    private AudioSource loopSource;    
    private AudioSource musicSource;   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            
            Destroy(gameObject);
            return;
        }

        // Make sound sources
        
        oneShotSource = GetComponent<AudioSource>();
        oneShotSource.playOnAwake = false;

        // 2. Döngüsel (Loop) Kaynak
        // Bu objeye yeni bir AudioSource bileşeni olarak ekliyoruz.
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;
        loopSource.playOnAwake = false;

        // 3. Müzik Kaynağı
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = 0.1f; // Varsayılan müzik sesi

    }


    public static void PlaySound(SoundType sound, float volume = 0.7f)
    {
        if (Instance == null) return; // Güvenlik kontrolü
        Instance.oneShotSource.PlayOneShot(Instance.soundList[(int)sound], volume);
    }

    /// <summary>
    /// Döngüsel bir ses çalar (Rüzgar, Motor vb.)
    /// </summary>
    public static void PlayLoop(SoundType sound, float volume = 1f)
    {
        if (Instance == null) return;
        Instance.loopSource.clip = Instance.soundList[(int)sound];
        Instance.loopSource.volume = volume;
        if (!Instance.loopSource.isPlaying)
            Instance.loopSource.Play();
    }


    public static void StopLoop()
    {
        if (Instance == null || !Instance.loopSource.isPlaying) return;
        Instance.loopSource.Stop();
        Instance.loopSource.clip = null;
    }

    /*
    public static void PlayMusic(float volume = 0.1f)
    {
        if (Instance == null) return;
        AudioClip musicClip = Instance.soundList[(int)SoundType.Music];
        if (musicClip == null) return;

        Instance.musicSource.clip = musicClip;
        Instance.musicSource.volume = volume;
        
        if (!Instance.musicSource.isPlaying)
            Instance.musicSource.Play();
    }
    */

   
    public static void StopMusic()
    {
        if (Instance == null || !Instance.musicSource.isPlaying) return;
        Instance.musicSource.Stop();
        Instance.musicSource.clip = null;
    }

  
    public static AudioClip GetClip(SoundType sound)
    {
        if (Instance == null) return null;
        return Instance.soundList[(int)sound];
    }
}