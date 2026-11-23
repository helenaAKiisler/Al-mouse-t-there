using UnityEngine;

public class IceCream : MonoBehaviour
{
    [SerializeField] private Transform objectAttachPoint;
    [SerializeField] public AudioClip woodenBridgeSoundClip;
    private ObjectAttachable objectAttachable;
    public bool isOpen;
    public bool hasHandle;
    
    // [SerializeField] private HUD hud;

    private void Start()
    {
        isOpen = false;
        hasHandle = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObjectAttachable attachable))
        {
            if (attachable.isAttached)
            {
                attachable.canAttach = false;
                return;
            }
            attachable.SetTargetAttachPoint(objectAttachPoint);
            attachable.SetCurrentStick(this);
            attachable.canAttach = true;
            Debug.Log("SET canAttach TRUE");

            // hud.OnInteract();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ObjectAttachable attachable))
        {
            attachable.canAttach = false;
            attachable.SetTargetAttachPoint(null);
            Debug.Log("SET canAttach FALSE");
            //hud.OnInteractEnd();

        }
    }
}
