using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBoatInteraction : MonoBehaviour
{
    public GameObject player;
    private ObjectDockable objectDockable;
    public Boat boat;
    public ObjectDockable dock;
    [SerializeField] private HUD hud;
    
    public bool onBoat = false;

    public float interactRange = 2f;
    public LayerMask interactableLayer;
    public bool seesDock = false;

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
        Debug.Log("OnMount called");
        print("OnMount");
        if (!seesDock)
        {
            Debug.Log("Not near a dock");
            return;
        }

        if (dock == null)
        {
            Debug.LogError("Dock reference is null!");
            return;
        }
        
        boat = dock.Mount();
        
        if (boat != null)
            {
                print("Boat Found");
                boat.MountBoat(player);
            }
        else
        {
            Debug.Log("No boat available at this dock.");
        }
        
        // TryMount();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Dock"))
        {
            seesDock = true;
            dock = collision.GetComponentInParent<ObjectDockable>();
            print("Dock entered");
            
            hud.OnDockInteraction();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Dock"))
        {
            seesDock = false;
            print("Dock exited");
            hud.OnDockEndInteraction();
        }
    }

    // void TryMount() 
        // {
        //     //Detects if collider is on 
        //     
        //     //Had to adjust offset origin upward, so that the ray casts from the waist height
        //     Vector3 rayOrigin = transform.position + Vector3.down * 0.2f;
        //     float sphereRadius = 0.6f; //Gives ray physical thickness
        //
        //     //Find nearby item
        //     if (Physics.SphereCast(rayOrigin, sphereRadius, transform.forward, out RaycastHit hitData, interactRange, interactableLayer))
        //     {
        //         Debug.Log("Hit object: " + hitData.collider.name);
        //         if (hitData.transform.TryGetComponent(out objectDockable))
        //         {
        //             boat = objectDockable.Mount();
        //             if (boat != null)
        //             {
        //                 print("Boat Mounted");
        //                 boat.MountBoat(player);
        //             }
        //             
        //         }
        //         
        //     }
        // }
    
}
