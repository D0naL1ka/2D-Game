using UnityEngine;

public class PlatformerSoundManager : MonoBehaviour
{
    public static PlatformerSoundManager Instance { get; private set; }

    [Header("Звукові ефекти")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip damageClip;
    [SerializeField] private AudioClip checkpointClip;
    [SerializeField] private AudioClip bounceClip;

    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        sfxSource = GetComponent<AudioSource>();
    }

    public void PlayJump() => sfxSource.PlayOneShot(jumpClip);
    public void PlayCoin() => sfxSource.PlayOneShot(coinClip);
    public void PlayDamage() => sfxSource.PlayOneShot(damageClip);
    public void PlayCheckpoint() => sfxSource.PlayOneShot(checkpointClip);
    public void PlayBounce() => sfxSource.PlayOneShot(bounceClip);

}
