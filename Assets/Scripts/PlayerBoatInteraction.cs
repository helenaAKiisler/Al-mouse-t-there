using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBoatInteraction : MonoBehaviour
{
    public GameObject player;
    private ObjectDockable objectDockable;
    public Boat boat;
    
    public float interactRange = 2f;
    public LayerMask interactableLayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMount(InputValue value)
    {
        print("OnMount");
        TryMount();
    }
    
    
    
    void TryMount() 
    {
        //Had to adjust offset origin upward, so that the ray casts from the waist height
        Vector3 rayOrigin = transform.position + Vector3.down * 0.2f;
        float sphereRadius = 0.6f; //Gives ray physical thickness

        //Find nearby item
        if (Physics.SphereCast(rayOrigin, sphereRadius, transform.forward, out RaycastHit hitData, interactRange, interactableLayer))
        {
            Debug.Log("Hit object: " + hitData.collider.name);
            if (hitData.transform.TryGetComponent(out objectDockable))
            {
                boat = objectDockable.Mount();
                if (boat != null)
                {
                    print("Boat Mounted");
                    boat.MountBoat(player);
                }
                
            }
            
        }
    }
}
