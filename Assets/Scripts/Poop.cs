using Tweens;
using Tweens.Core;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField] private Transform poop;
    private Boat boat;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat")) {
            boat = other.GetComponent<Boat>();

            if (boat != null && boat.NPCOnBoat)
            {
                //idk roach talking
                eatingPoop();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }


    TweenInstance eatingPoop(float dur = 5)
    {
        //Scaling poop down
        var tween = new LocalScaleXTween
        {
            to = 0.2f,//Expects 1 value for y
            duration = dur,
            onEnd = (instance) =>
            {
                //runs right when it reaches 0 scale!
                Destroy(gameObject);
            },
        };


        TweenInstance inst = poop.gameObject.AddTween(tween);
        return inst;
    }
}
