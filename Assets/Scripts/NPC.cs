using TMPro;
using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
public class NPC : MonoBehaviour
{
    public enum NPCState { Idle, Moving, FollowingPlayer, PerformingAction }

    [SerializeField] private NPCState state = NPCState.Idle;   // visible in the Inspector for debugging
    public NPCState CurrentState => state;
    
    public Transform player;
    public PlayerPickUp playerPickUp;
    public DialogueController dialogueController;
    [SerializeField] private Transform cockroachModel;
    [SerializeField] private TMP_Text text;                   
    
    public Quest quest;
    [SerializeField] private string requiredItemId = "quest_item";
    [TextArea] [SerializeField] private string acceptedText = "Great! Please bring me the item I need.";
    [TextArea] [SerializeField] private string missingItemText = "I need something from you. Bring it to me!";
    [TextArea] [SerializeField] private string itemReceivedText = "Thank you! Lead the way, I'm right behind you.";

    private NPCMovement movement;
    private Quaternion idleRotation;
    private Quaternion followRotation;

    // Quest state in which the NPC should run to the stairs when the water rises
    public bool CanReactToWater =>
        quest.currentState == Quest.QuestState.NotStarted ||
        quest.currentState == Quest.QuestState.WaitingForAnswer ||
        quest.currentState == Quest.QuestState.QuestStarted;

    private void Awake()
    {
        movement = GetComponent<NPCMovement>();
        //Rotation of the NPC when idle and following the player
        idleRotation = Quaternion.Euler(90f, 0f, 9f);
        followRotation = Quaternion.Euler(90f, 0f, 0f);

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    private void Start()
    {
        text.gameObject.SetActive(false);
        SetState(NPCState.Idle);
    }

    private void Update()
    {
        if (state == NPCState.FollowingPlayer)
            movement.Follow(player);
        else if (state == NPCState.Moving && movement.HasArrived)
            SetState(NPCState.Idle);
    }
    
    public void SetState(NPCState newState)
    {
        state = newState;
        cockroachModel.localRotation = newState == NPCState.FollowingPlayer ? followRotation : idleRotation;
    }

    public bool MoveTo(Transform target)
    {
        if (target == null) return false;
        if (!movement.MoveTo(target.position)) return false;
        SetState(NPCState.Moving);
        return true;
    }

    //PlayerInteractor functions
    public void OnPlayerEnter()
    {
        if (!dialogueController.IsOpen && state != NPCState.FollowingPlayer)
            text.gameObject.SetActive(true);
    }

    public void OnPlayerExit()
    {
        text.gameObject.SetActive(false);
        dialogueController.CloseDialogue();
    }

    //NPC interactions
    public void Interact()
    {
        if (state == NPCState.PerformingAction) return;   // busy with an activity (tunnel, later)
        if (dialogueController.IsOpen) return;

        text.gameObject.SetActive(false);
        switch (quest.currentState)
        {
            case Quest.QuestState.NotStarted:
                dialogueController.StartDialogue(this);
                break;
            case Quest.QuestState.WaitingForAnswer:
                dialogueController.ShowQuestionAgain(this);
                break;
            case Quest.QuestState.QuestStarted:
                if (!TryReceiveItem(playerPickUp))
                    dialogueController.ShowMessage(this, missingItemText);
                break;
            case Quest.QuestState.ReadyToFollow:
                SetState(NPCState.FollowingPlayer);  
                break;
            case Quest.QuestState.Completed:
                dialogueController.ShowCompletedQuestDialogue(this);
                break;
        }
    }

    //Quest
    public void OfferQuest()
    {
        if (quest.currentState == Quest.QuestState.NotStarted)
            quest.currentState = Quest.QuestState.WaitingForAnswer;
    }

    public void AcceptQuest()
    {
        quest.currentState = Quest.QuestState.QuestStarted;
        dialogueController.ShowMessage(this, acceptedText);
    }

    // npc receiving object(worked at some point)
    public bool TryReceiveItem(PlayerPickUp giver)
    {
        if (quest.currentState != Quest.QuestState.QuestStarted || giver == null) return false;
    
        ObjectGrabbable held = giver.HeldObject;
        if (held == null ||
            !held.TryGetComponent(out QuestItem item) ||
            item.ItemId != requiredItemId)
            return false;
    
        giver.TakeHeldObject();     
        Destroy(held.gameObject);   
    
        quest.currentState = Quest.QuestState.ReadyToFollow;
        SetState(NPCState.FollowingPlayer);
        text.gameObject.SetActive(false);
        dialogueController.ShowMessage(this, itemReceivedText);
        return true;
    }

    // used later
    public void CompleteQuest()
    {
        quest.currentState = Quest.QuestState.Completed;
        SetState(NPCState.Idle);
    }
}
