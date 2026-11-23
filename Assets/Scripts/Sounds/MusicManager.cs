using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Audio Source--")]
    [SerializeField] AudioSource ambianceSource;
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clips---")]
    public AudioClip ambiance;
    public AudioClip BGMusic;

    private void Start()
    {
        ambianceSource.clip = ambiance;
        ambianceSource.Play();
        musicSource.clip = BGMusic;
        musicSource.Play();
    }
}
