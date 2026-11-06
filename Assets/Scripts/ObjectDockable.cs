using UnityEngine;

public class ObjectDockable : MonoBehaviour
{
    
    public bool HasBoat = false;
    
    private GameObject dock;
    public Transform waterPlaneLevel;
    public Transform boat;
    public Transform boatLocation;
    public Transform playerLocation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dock = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Transform GetBoatLocation()
    {
        Vector3 level = boatLocation.position;
        if (WaterLevels.Instance != null)
        {

            level.y = WaterLevels.Instance.transform.position.y;

        } else if (waterPlaneLevel != null)
        {
            level.y = waterPlaneLevel.transform.position.y;
        }
        boatLocation.position = level;

        return boatLocation; 
    }
    
    public Boat Mount()
    {
        print("MOUNT FUCNTION");
        if (!HasBoat)
        {
            print("Problem");
            return null;
        }
        print("reached");
        Boat boatComponent = boat.GetComponent<Boat>();
        print(boatComponent);
        boat.SetParent(null, true);
        HasBoat = false;
        return boatComponent;
    }

    public void Dock(GameObject boatObject)
    {
        boat = boatObject.transform;
        print("Dock FUCTION");
        if (!HasBoat)
        {
            print("Problem");
        }
        boat.SetParent(boatLocation, true);
        boat.localPosition = Vector3.zero;
        boat.localRotation = Quaternion.identity;
        HasBoat = true;
    }
}
