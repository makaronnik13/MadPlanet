using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    public AudioClipPair QuestCompleteSound;
    public TMPro.TextMeshProUGUI Counter, Title;
    public Image Img;
    public GameObject CounterHab;
    private QuestInstance Quest;

    public void Show(QuestInstance quest)
    {
        Quest = quest;
        Title.text = quest.Quest.Title.Text;
        CounterHab.SetActive(quest.Counter.MaxValue!=0);
        Img.sprite = quest.Quest.Icon;
        if (quest.Counter.MaxValue != 0)
        {
            quest.Counter.Current.AddListener(CounterChanged, true);
        }
        quest.Completed.AddListener(QuestStateChanged, true);
    }

    private void QuestStateChanged(int completed)
    {
        if (completed == 2)
        {
            SoundsPlayer.Instance.PlaySound(QuestCompleteSound);
            Destroy(gameObject, 2);
        }
    }

    private void CounterChanged(int v)
    {
        Counter.text = v + "/" + Quest.Counter.MaxValue;
    }

    private void Update()
    {
        
    }
}
