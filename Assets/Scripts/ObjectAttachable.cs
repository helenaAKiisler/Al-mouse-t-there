using UnityEngine;

public class ObjectAttachable : ObjectGrabbable
{
    private Transform objectAttachPoint;
    public bool canAttach = false;
    public Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    //Attach point given to us by sewer lid
    public void SetTargetAttachPoint(Transform targetAttachPoint)
    {
        this.objectAttachPoint = targetAttachPoint;
    }

    public void Attach()
    {
        if (canAttach && objectAttachPoint != null)
        {
            //Stays in place
            body.isKinematic = true;
            body.useGravity = false;
            transform.position = objectAttachPoint.position;
            //transform.rotation = targetAttachPoint.rotation;
        }
    }
}
