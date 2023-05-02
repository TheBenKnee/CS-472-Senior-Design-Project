using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo
{
    public QuestInfo(string requiredName, int requiredQuantity, string rewardName, int rewardQuantity)
    {
        requiredItemName = requiredName;
        requiredItemQuantity = requiredQuantity;
        rewardItemName = rewardName;
        rewardItemQuantity = rewardQuantity;
    }

    public string requiredItemName;
    public int requiredItemQuantity;

    public string rewardItemName;
    public int rewardItemQuantity;
}

public class QuestManager : MonoBehaviour
{
    // First string, int is name and quantity of required item, second is for reward
    private List<QuestInfo> possibleQuests = new List<QuestInfo>
    {
        new QuestInfo("Wood", 64, "Bread", 16),
        new QuestInfo("Wood", 32, "Bread", 4),
        new QuestInfo("Wood", 16, "Bread", 1),
        new QuestInfo("Berry", 32, "Bread", 5),
        new QuestInfo("Honey", 5, "Bread", 5)
    };

    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> pendingQuests = new List<Quest>();

    [SerializeField] private Colony myColony;

    [SerializeField] private float newQuestDurationSeconds = 10.0f;

    private float timeSinceLastQuest = 0f;

    [ContextMenu("PrintPendingQuests")]
    public void PrintPendingQuests()
    {
        foreach(Quest quest in pendingQuests)
        {
            quest.PrintQuest();
        }
    }

    [ContextMenu("PrintPendingQuestNumber")]
    public void PrintPendingQuestNumber()
    {
        Debug.Log("Pending Quests left: " + pendingQuests.Count);
    }

    private void OnEnable()
    {
        CreateRandomQuest();
        CreateRandomQuest();
        CreateRandomQuest();
    }

    public void CancelAllQuests()
    {
        List<Quest> tmpList = new List<Quest>();
        foreach (Quest quest in activeQuests)
        {
            tmpList.Add(quest);
        }

        foreach (Quest quest in tmpList)
        {
            RemoveActiveQuest(quest);
        }
    }

    public List<Quest> GetActiveQuestList()
    {
        return activeQuests;
    }

    public List<Quest> GetPendingQuestList()
    {
        return pendingQuests;
    }

    public void AcceptQuest(Quest acceptedQuest)
    {
        activeQuests.Add(acceptedQuest);
        pendingQuests.Remove(acceptedQuest);
    }

    public void DeclineQuest(Quest declinedQuest)
    {
        pendingQuests.Remove(declinedQuest);
    }

    public void RemoveActiveQuest(Quest activeQuest)
    {
        activeQuests.Remove(activeQuest);
    }

    public void CheckQuestCompletion()
    {
        foreach (Quest quest in activeQuests)
        {
            quest.CheckCondition();
        }
    }

    public void CreateRandomQuest()
    {
        // Getting a random index to generate a random quest
        int questNumber = Random.Range(0, possibleQuests.Count);
        QuestInfo questInfo = possibleQuests[questNumber];

        pendingQuests.Add(new Quest(questInfo.rewardItemName, questInfo.rewardItemQuantity, questInfo.requiredItemName, questInfo.requiredItemQuantity, myColony));
    }

    private void Update() 
    {
        timeSinceLastQuest += Time.timeScale / 50f;

        if(timeSinceLastQuest > newQuestDurationSeconds)
        {
            bool pawnQuestExists = false;

            foreach(Quest quest in pendingQuests)
            {
                if(quest.GetRewardItemName() == "Pawn")
                {
                    pawnQuestExists = true;
                    break;
                }
            }

            if(!pawnQuestExists)
            {
                pendingQuests.Add(new Quest("Pawn", 1, "Coin", 5, myColony));
            }

            timeSinceLastQuest = 0f;
        }
    }
}




