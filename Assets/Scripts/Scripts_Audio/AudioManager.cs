using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //// Biến lưu Audio Surce
    //[SerializeField] AudioSource musicAudioSource;
    //[SerializeField] AudioSource vfxAudioSource;

    //// biến lưu Audio clip
    //[SerializeField] AudioClip musicClip;
    //public AudioClip shotClip;
    //public AudioClip expClip;

    //private void Start()
    //{
    //    musicAudioSource.clip = musicClip;
    //    musicAudioSource.Play();
    //}
    //public void PlaySfx( AudioClip sfxClip)
    //{
    //    vfxAudioSource.clip = sfxClip;
    //    vfxAudioSource.PlayOneShot(sfxClip);
    //}
    //public void PlayMusic()
    //{
    //    musicAudioSource.Play();
    //}

    //public bool MusicIsPlaying()
    //{
    //    return musicAudioSource.isPlaying;
    //}


    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip musicClip;
    public AudioClip expClip;
    public AudioClip shotClip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.volume = 1f; // ban đầu full
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        musicSource.Play();

        sfxSource.volume = PlayerPrefs.GetFloat("SfxVolume", 1f);
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float v)
    {
        // clamp01(v) : viết tắt của clamp(v,0f,1f) => giới hạn v trong từ 0->1
        float volume = Mathf.Clamp01(v);
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSfxVolume(float v)
    {
        float volume = Mathf.Clamp01(v);
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }
    public void MuteMusic()
    {
        musicSource.volume = 0f;
        PlayerPrefs.SetFloat("MusicVolume", 0f);
    }
    public void UnMuteMusic()
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        musicSource.volume = volume > 0f ? volume : 1f;
    }
    public void MuteSfx()
    {
        sfxSource.volume = 0f;
        PlayerPrefs.SetFloat("SfxVolume", 0f);
    }

    public void UnmuteSfx()
    {
        float volume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        sfxSource.volume = volume > 0f ? volume : 1f;
    }
}
