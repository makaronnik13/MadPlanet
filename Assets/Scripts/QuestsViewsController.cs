using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsViewsController : MonoBehaviour
{
    public GameObject QuestViewPrefab;

    public void Start()
    {
        Game.Instance.OnQuestTaken += QuestTaken;

        foreach (QuestInstance qi in Game.Instance.gameData.Quests)
        {
            if (qi.Completed.Value == 1)
            {
                QuestTaken(qi);
            }
        }
    }

    private void OnDestroy()
    {
        Game.Instance.OnQuestTaken -= QuestTaken;
    }

    private void QuestTaken(QuestInstance q)
    {
        Debug.Log("quest taken");
        GameObject newQuestView = Instantiate(QuestViewPrefab);
        newQuestView.transform.SetParent(transform);
        newQuestView.transform.localPosition = Vector3.zero;
        newQuestView.transform.localScale = Vector3.one;
        newQuestView.GetComponent<QuestView>().Show(q);
    }
}
