using System.Collections;
using UnityEngine;

public class PlayerSqueaks : MonoBehaviour
{
    [SerializeField] AudioClip[] squeaks;

    private float squeakChance = 0.1f;
    private float cooldown = 5f;
    private float volume = 1f; //full

    private void Start()
    {
        StartCoroutine(SqueaksHereAndThere());
    }

    private IEnumerator SqueaksHereAndThere()
    {
        while (true)
        {
            //Waits for cooldown seconds
            yield return new WaitForSeconds(cooldown);

            //checking the chance!
            if (squeakChance >= Random.value)
            {
                SoundFXManager.instance.PlayRandomSoundFXClip(squeaks, transform, volume);
            }
        }
    }
}
