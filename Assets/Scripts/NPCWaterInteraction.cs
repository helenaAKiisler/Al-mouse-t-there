using UnityEngine;

[RequireComponent(typeof(NPC))]
public class NPCWaterReaction : MonoBehaviour
{
    [SerializeField] private WaterLevels waterLevels;
    [SerializeField] private Transform stairLevel1;
    [SerializeField] private Transform stairLevel2;

    private NPC npc;
    private bool movedToLevel1;
    private bool movedToLevel2;

    private void Awake()
    {
        npc = GetComponent<NPC>();
    }

    private void Update()
    {
        if (waterLevels == null || !npc.CanReactToWater) return;

        if (waterLevels.isMove && !movedToLevel1)
        {
            movedToLevel1 = true;
            npc.MoveTo(stairLevel1);
        }

        if (waterLevels.isLvl2 && !movedToLevel2)
        {
            movedToLevel2 = true;
            npc.MoveTo(stairLevel2);
        }
    }
}