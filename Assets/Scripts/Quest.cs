using UnityEngine;
[System.Serializable]
public class Quest
{
    public string questName;
    public string questDescription;

    public bool isStarted;
    public bool isCompleted;

    public void StartQuest()
    {
        isStarted = true;
        Debug.Log("Quest Started");
    }

    public void CompleteQuest()
    {
        isCompleted = true;
        Debug.Log("Quest Completed");
    }
}
