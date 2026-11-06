using UnityEngine;

[RequireComponent(typeof(NPC))]
public class NPCWaterReaction : MonoBehaviour
{
    [SerializeField] private WaterLevels waterLevels;
    [SerializeField] private Transform stairLevel1;
    [SerializeField] private Transform stairLevel2;

    private NPC npc;
    private int handledLevel; 
    private int CurrentLevel => waterLevels.isLvl2 ? 2 : waterLevels.isMove ? 1 : 0;
    private void Awake()
    {
        npc = GetComponent<NPC>();
    }

    private void Update()
    {
        if (waterLevels == null || !npc.CanReactToWater) return;

        int level = CurrentLevel;
        if (level <= handledLevel) return;   // nothing new

        Transform target = level == 1 ? stairLevel1 : stairLevel2;
        Debug.Log($"Water level {level}: isMove={waterLevels.isMove}, isLvl2={waterLevels.isLvl2}");

        if (npc.MoveTo(target))
            handledLevel = level;            
    }
    
}