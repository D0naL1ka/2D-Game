using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer gameMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambienceSlider;

    private void Start()
    {
        // початкові позиції слайдерів
        if (musicSlider != null) musicSlider.value = 0.75f;
        if (sfxSlider != null) sfxSlider.value = 0.75f;
        if (ambienceSlider != null) ambienceSlider.value = 0.75f;

        StartCoroutine(InitVolume());
    }

    private IEnumerator InitVolume()
    {
        yield return null;
        SetMusicVolume(0.75f);
        SetSFXVolume(0.75f);
        SetAmbienceVolume(0.75f);
    }

    public void SetMusicVolume(float value)
    {
        float db = value > 0.0001f ? Mathf.Log10(value) * 20f : -80f;
        gameMixer.SetFloat("MusicVolume", db);
    }

    public void SetSFXVolume(float value)
    {
        float db = value > 0.0001f ? Mathf.Log10(value) * 20f : -80f;
        gameMixer.SetFloat("SFXVolume", db);
    }

    public void SetAmbienceVolume(float value)
    {
        float db = value > 0.0001f ? Mathf.Log10(value) * 20f : -80f;
        gameMixer.SetFloat("AmbienceVolume", db);
    }
}