using com.armatur.common.flags;
using System.Linq;
using System;
using com.armatur.common.serialization;

[Serializable]
public class QuestInstance
{
    [Savable]
    [SerializeData("QuestId", FieldRequired.False)]
    private int _questId = -1;

    public Quest Quest
    {
        get
        {
            return DefaultRessources.GetQuest(_questId);
        }
    }

    [Savable]
    [SerializeData("State", FieldRequired.False)]
    public GenericFlag<int> Completed = new GenericFlag<int>("State", 1);
    [Savable]
    [SerializeData("Counter", FieldRequired.False)]
    public CounterWithMax Counter = new CounterWithMax("Counter");

    public QuestInstance()
    {

    }

    public QuestInstance(Quest quest)
    {
        _questId = DefaultRessources.Quests.ToList().IndexOf(quest);
        Counter.ChangeMax(quest.MaxValue);
        Counter.Change(0);
        Completed.SetState(1);
    }

    public void Progress(int v = 1)
    {
        Counter.Change(v);
        if (Counter.CurrentValue == Counter.MaxValue)
        {
            Completed.SetState(2);
        }
    }
}