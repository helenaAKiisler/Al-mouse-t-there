using UnityEngine;

public class ObjectAttachable : ObjectGrabbable
{
    private Transform objectAttachPoint;
    public bool canAttach = false;
    public Rigidbody body;
    public SewerLid currentSewerLid;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
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

    public void Attach()
    {
        if (canAttach && objectAttachPoint != null)
        {
            //Set it as child of attach point
            transform.SetParent(objectAttachPoint);

            //Stays in place - freeze physics
            body.isKinematic = true;
            body.useGravity = false;

            //Snapping position and rotation locally - relative to parent
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;

            //Notify sewerlid that it has handle
            if (currentSewerLid != null)
            {
                currentSewerLid.hasHandle = true;
            }
        }
    }
}
