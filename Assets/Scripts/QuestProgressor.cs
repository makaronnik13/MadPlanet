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
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        while (Game.Instance == null || Game.Instance.gameData == null)
        {
            yield return null;
        }

        if (Quest)
        {
            if (Game.Instance.gameData.Quests.FirstOrDefault(q => q.Quest == Quest) != null)
            {
                QuestCompleted(Game.Instance.gameData.Quests.FirstOrDefault(q => q.Quest == Quest).Completed.Value);
                Game.Instance.gameData.Quests.FirstOrDefault(q => q.Quest == Quest).Completed.AddListener(QuestCompleted);
            }
            else
            {
                Game.Instance.OnQuestTaken += QuestTaken;
            }
        }
    }

    private void QuestTaken(QuestInstance q)
    {
        if (q.Quest == Quest)
        {
            q.Completed.AddListener(QuestCompleted);
        }
    }

    private void QuestCompleted(int v)
    {
        if (v == 2)
        {
            OnQuestCompleted.Invoke();
        }
    }
}
