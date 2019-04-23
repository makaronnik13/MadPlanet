using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour
{
    public Dialog[] Dialogues;

    private Dialog _currntDialogue;

    public void Start()
    {
        _currntDialogue = Dialogues[0];
    }

    public void PlayDialog()
    {
        DialogPlayer.Instance.PlayDialogue(_currntDialogue);
    }

    public void ReplaceDialogue()
    {

    }
}
