using UnityEngine;

//This will be a singleton - access in any class
public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;


    private void Awake()
    {
        if (instance == null) 
        {
            instance = this; 
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //Spawn in gameobject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //Assign audioClip
        audioSource.clip = audioClip;

        //Assign volume
        audioSource.volume = volume;

        //Play sound
        audioSource.Play();

        //Get length of clip
        float clipLength = audioSource.clip.length;

        //Destroy clip after it is done playing
        Destroy(audioSource.gameObject, clipLength); //afer clip length
    }
}
