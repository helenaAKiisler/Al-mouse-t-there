using System;
using System.Collections;
using UnityEngine;

public class SewerLid : MonoBehaviour
{
    [SerializeField] private Transform objectAttachPoint;
    private ObjectAttachable objectAttachable;
    public bool isOpen;
    public bool hasHandle;
    public bool isIce;

    private void Start()
    {
        isOpen = false;
        hasHandle = false;
        isIce = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObjectAttachable attachable)) 
        {
            attachable.SetTargetAttachPoint(objectAttachPoint);
            attachable.SetCurrentSewerLid(this);
            attachable.canAttach = true;

            if (hasHandle) 
            {
                OpenDoor();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ObjectAttachable attachable))
        {
            attachable.canAttach = false;
            attachable.SetTargetAttachPoint(null);

        }
    }

    public void OpenDoor()
    {
        //If it's already open
        if (isOpen)
        {
            return; //do nothing
        }
        isOpen = true;
        //Starting coroutine
        StartCoroutine(RotateHingeRoutine());
    }


    //Coroutine method itself
    private IEnumerator RotateHingeRoutine()
    {
        Quaternion startRotate = transform.rotation;
        Quaternion endRotate = startRotate * Quaternion.Euler(0f, 0f, -90f); //should swing 90 degrees

        float timeElapsed = 0f;
        float duration = 2f; //takes 1 second to swing open

        //Loop runs across frames
        while (timeElapsed < duration) 
        {
            // Smoothly interpolate rotation
            transform.rotation = Quaternion.Slerp(startRotate, endRotate, timeElapsed/duration);

            //Not to forget to advance time
            timeElapsed += Time.deltaTime;

            //Pauses execution and waits for next frame before looping again
            yield return null;
        }

        //Ensuring it lands on the final rotation when finished
        transform.rotation = endRotate;
    }
}
