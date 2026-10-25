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
            //Stays in place
            body.isKinematic = true;
            body.useGravity = false;
            transform.position = objectAttachPoint.position;
            transform.rotation = objectAttachPoint.rotation;

            //Notify sewerlid that it has handle
            if (currentSewerLid != null)
            {
                currentSewerLid.hasHandle = true;
            }
        }
    }
}
