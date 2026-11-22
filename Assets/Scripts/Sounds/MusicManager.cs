using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Audio Source--")]
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clips---")]
    public AudioClip ambiance;

    private void Start()
    {
        musicSource.clip = ambiance;
        musicSource.Play();
    }
}
