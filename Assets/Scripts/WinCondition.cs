using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public HUD hud;
    public NPC npc;
    private bool gameOver = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        if (npc.quest.currentState == Quest.QuestState.Completed)
        {
            gameOver = true;
            hud.gameEnd(true);
        }
    }
    
}
