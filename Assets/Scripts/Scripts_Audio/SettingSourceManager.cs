using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingSourceManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public Toggle musicToggle;
    public Toggle sfxToggle;

    [SerializeField] private GameObject iconOnMusic;
    [SerializeField] private GameObject iconOnSFX;
    [SerializeField] private GameObject iconOffMusic;
    [SerializeField] private GameObject iconOffSFX;


    private void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);

        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSfxVolume;

        // Nếu âm lượng đã lưu lớn hơn 0 → bật toggle. Ngược lại → tắt toggle.
        musicToggle.isOn = savedMusicVolume > 0f;
        sfxToggle.isOn = savedSfxVolume > 0f;

        // gắn một hàm callback (hàm được gọi lại) khi giá trị của một UI component thay đổi,
        // ví dụ: Slider, Toggle, InputField, Dropdown...
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSfxVolume);

        musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        sfxToggle.onValueChanged.AddListener(OnSfxToggleChanged);
    }
    void OnMusicToggleChanged(bool isOn)
    {
        if (isOn)
        {
            float volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            if (volume == 0f) volume = 1f; // Nếu trước đó tắt → gán lại 1f
            musicSlider.value = volume;
            AudioManager.Instance.SetMusicVolume(volume);

            iconOnMusic.SetActive(true);
            iconOffMusic.SetActive(false);
        }
        else
        {
            musicSlider.value = 0f;
            AudioManager.Instance.SetMusicVolume(0f);
            
            iconOnMusic.SetActive(false);
            iconOffMusic.SetActive(true);
        }
    }

    void OnSfxToggleChanged(bool isOn)
    {
        if (isOn)
        {
            float volume = PlayerPrefs.GetFloat("SfxVolume", 1f);
            if (volume == 0f) volume = 1f;
            sfxSlider.value = volume;
            AudioManager.Instance.SetSfxVolume(volume);

            iconOnSFX.SetActive(true);
            iconOffSFX.SetActive(false);
        }
        else
        {
            sfxSlider.value = 0f;
            AudioManager.Instance.SetSfxVolume(0f);

            iconOffSFX.SetActive(true);
            iconOnSFX.SetActive(false);
        }
    }
}
