using System;
using UnityEngine;

public class SewerLid : MonoBehaviour
{
    [SerializeField] private Transform objectAttachPoint;
    private ObjectAttachable objectAttachable;
    public bool isOpen;

    private void Start()
    {
        isOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObjectAttachable attachable)) 
        {
            attachable.SetTargetAttachPoint(objectAttachPoint);
            attachable.canAttach = true;
            if ()
            
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

    }

}
