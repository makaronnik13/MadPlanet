using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour
{
    public Dialog[] Dialogues;

    private Dialog _currntDialogue;

    public bool Random = false;

    public void Awake()
    {
        _currntDialogue = Dialogues[0];
    }

    public void PlayDialog()
    {
        if (Random)
        {
            int r = UnityEngine.Random.Range(0, Dialogues.Length);
            Debug.Log(r);
            _currntDialogue = Dialogues[r];
        }

        DialogPlayer.Instance.PlayDialogue(_currntDialogue);
    }

    public void PlayRandom()
    {

    }
}
