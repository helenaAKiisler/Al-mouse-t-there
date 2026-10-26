using UnityEngine;

[System.Serializable]
public class Quest
{

    public enum QuestState
    {
        NotStarted,
        WaitingForAnswer,
        QuestStarted,
        ReadyToFollow,
        Completed,
    }

    public QuestState currentState = QuestState.NotStarted;
}
