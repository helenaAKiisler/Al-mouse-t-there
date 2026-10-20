using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNPCInteractor : MonoBehaviour
{
    private Vector3 p_pos;
    //Need actual NPC INSTANCE!!!
    public NPC npc;
    public Vector3 npc_pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTalk(InputValue value) 
    {
        if (npc.player_detected) { 
            Debug.Log("Talking");
            //Trigger npc monologeu/script
            npc.Talk();
        }
        else
        {
            Debug.Log("NPC out of range");
        }
    }
    //Lowk seda pole vaja vist
    // private bool DistanceFromNpc() 
    // {
    //     p_pos = transform.position;
    //     npc_pos = transform.position; //Have to change this later for actual npc instance
    //
    //     if (Vector3.Distance(npc_pos, p_pos) <= 10f)
    //     {
    //         return true;
    //     }
    //
    //     
    //     return false;
    // }


    
}
