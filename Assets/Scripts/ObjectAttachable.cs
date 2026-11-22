using UnityEngine;

public class ObjectAttachable : ObjectGrabbable
{
    [SerializeField] private GameObject triggerCollider;
    private Transform objectAttachPoint;
    public bool canAttach = false;
    public Rigidbody body;
    public SewerLid currentSewerLid;
    public IceCream currectStick;
    public bool showingHud = false;
    public bool isAttached = false;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canAttach)
        {
            Debug.Log("CAN ATTACH");
        }

        if (!canAttach)
        {
            Debug.Log("CAN NOT ATTACH");
        }
        if (canAttach && !showingHud)
        {
            Debug.Log("SHOWING ATTACHABLE HUD");
            Hud.OnInteract();
            showingHud = true;
        }
        else if (!canAttach && showingHud)
        {
            Debug.Log("HIDING ATTACHABLE HUD");
            Hud.OnInteractEnd();
            showingHud = false;
        }
        
    }

    //Attach point given to us by sewer lid
    public void SetTargetAttachPoint(Transform targetAttachPoint)
    {
        this.objectAttachPoint = targetAttachPoint;
    }

    public void SetCurrentSewerLid(SewerLid lid)
    {
        this.currentSewerLid = lid;
    }

    public void SetCurrentStick(IceCream stick)
    {
        this.currectStick = stick;
    }

    public void Attach()
    {
        if (canAttach && objectAttachPoint != null)
        {
            isAttached = true;
            //Set it as child of attach point
            transform.SetParent(objectAttachPoint);

            //Stays in place - freeze physics
            body.isKinematic = true;
            body.useGravity = false;

            //Snapping position and rotation locally - relative to parent
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            //transform.rotation = Quaternion.identity;

            //Notify sewerlid that it has handle
            if (currentSewerLid != null && !currentSewerLid.isIce)
            {
                currentSewerLid.hasHandle = true;
            }

            if (currectStick != null)
            {
                WaterLevels.Instance.isLvl2 = true;
                WaterLevels.Instance.isMove = true;
                
                
            }
            if(triggerCollider!=null)
            {
                triggerCollider.SetActive(false);
            }
            Hud.OnInteractEnd();
            showingHud = false;
            canAttach = false;
        }
    }
}
