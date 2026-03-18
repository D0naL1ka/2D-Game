using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip calmMusic;
    [SerializeField] private AudioClip battleMusic;
    [SerializeField] private float fadeDuration = 1.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void SwitchToBattle() => StartCoroutine(CrossFade(battleMusic));
    public void SwitchToCalm() => StartCoroutine(CrossFade(calmMusic));

    private IEnumerator CrossFade(AudioClip newClip)
    {
        float startVolume = musicSource.volume;
        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }
        musicSource.clip = newClip;
        musicSource.Play();
        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, startVolume, t / fadeDuration);
            yield return null;
        }
        musicSource.volume = startVolume;
    }
}
