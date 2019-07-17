using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class QuestProgressor : MonoBehaviour
{
    public Quest Quest;
    public UnityEvent OnQuestCompleted;

    public void Progress()
    {
        Game.Instance.gameData.Quests.FirstOrDefault(q => q.Quest == Quest).Progress(1);
    }

    private void Start()
    {
        if (Quest)
        {
          
            if (Game.Instance.gameData.Quests.FirstOrDefault(q=>q.Quest == Quest)!=null)
            {
                Debug.Log("check "+Quest.name);
                QuestCompleted(Game.Instance.gameData.Quests.FirstOrDefault(q => q.Quest == Quest).Completed.Value);
                Game.Instance.gameData.Quests.FirstOrDefault(q => q.Quest == Quest).Completed.AddListener(QuestCompleted);
            }
            else
            {
                Debug.Log("listen " + Quest.name);
                Game.Instance.OnQuestTaken += QuestTaken;
            }
        }
    }

    private void QuestTaken(QuestInstance q)
    {
        if (q.Quest == Quest)
        {
            Debug.Log("add listener " + Quest.name);
            q.Completed.AddListener(QuestCompleted);
        }
    }

    private void QuestCompleted(int v)
    {
        Debug.Log(Quest.name+"/"+v);
        if (v == 2)
        {
            OnQuestCompleted.Invoke();
        }
    }
}
