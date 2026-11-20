using UnityEngine;

public class IceCreamStick : MonoBehaviour
{
    [SerializeField] private Transform objectAttachPoint;
    private ObjectAttachable objectAttachable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObjectAttachable attachable)) 
        {
            attachable.SetTargetAttachPoint(objectAttachPoint);
            attachable.canAttach = true;
            
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
}
