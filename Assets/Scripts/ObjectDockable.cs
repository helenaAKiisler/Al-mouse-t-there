using UnityEngine;

public class ObjectDockable : MonoBehaviour
{
    
    private bool HasBoat = false;
    
    private GameObject dock;
    public Transform boat;
    public Transform boatLocation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dock = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Boat Mount()
    {
        if (!HasBoat || boat == null)
        {
            return null;
        }
        Boat boatComponent = dock.GetComponent<Boat>();
        boat.SetParent(null, true);
        HasBoat = false;
        return boatComponent;
    }
}
