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
        if (gameOver) return;
        float waterY = waterLevel.transform.position.y;
        Debug.Log(
            "Head: " + headPoint.position.y +
            " Water: " + waterY
        );
        
        
        //if water has reached the player --> player has fallen into the water
        if (headPoint.position.y <= waterY)
        {
            gameOver = true;
            hud.gameEnd(false);
        }
    }
}
