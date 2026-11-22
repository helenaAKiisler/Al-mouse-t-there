using UnityEngine;

public class LoseCondition : MonoBehaviour
{
    public Transform headPoint;
    //public Transform player;
    public WaterLevels waterLevel;

    public HUD hud;
    
    private bool gameOver = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("LOSE CONDITION RUNNING");
        if (gameOver) return;
        //float waterY = waterLevel.transform.position.y;
        
        //if water has reached the player --> player has fallen into the water
        // if (headPoint.position.y <= waterY)
        // {
        //     Debug.Log("LOSE!");
        //     gameOver = true;
        //     hud.gameEnd(false);
        // }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (gameOver) return;
    //
    //     if (other.CompareTag("Water"))
    //     {
    //         gameOver = true;
    //         hud.gameEnd(false);
    //     }
    // }
}
