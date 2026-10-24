using UnityEngine;

public class Climbable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.GetComponent<PlayerClimbing>())
            {
                //Player can now climb
                other.gameObject.GetComponent<PlayerClimbing>().isClimbing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.GetComponent<PlayerClimbing>())
            {
                //Player can no longer climb
                other.gameObject.GetComponent<PlayerClimbing>().isClimbing = true;
            }
        }
    }
}
