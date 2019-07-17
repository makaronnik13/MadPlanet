using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueSystem/Dialogue")]
public class Dialog : ScriptableObject
{
    public List<DialoguePhrase> Phrases;
    public Quest QuestOnComplete;
}