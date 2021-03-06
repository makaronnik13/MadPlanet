﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPlayer : MonoBehaviour
{
    public static DialogPlayer Instance;

    public Platformer2DUserControl MovementController;

    [SerializeField]
    private Typewriter Typewriter;
    [SerializeField]
    private GameObject TextPanel;
    [SerializeField]
    private GameObject LeftPerson, RightPerson;
    [SerializeField]
    private Image LeftPortrait, RightPortrait;
    [SerializeField]
    private TMPro.TextMeshProUGUI LeftName, RightName;

    public Action<bool> OnPlayingStateChanged = (v) => { };

    private bool _playing = false;
    private bool Playing
    {
        get
        {
            return _playing;
        }
        set
        {
            _playing = value;
            OnPlayingStateChanged(_playing);
            MovementController.enabled = !_playing;
        }
    }
    private Dialog _playingDialog;
    private int _phraseId;

    private void Start()
    {
        Instance = GetComponent<DialogPlayer>();
    }

    public void PlayDialogue(Dialog dialog)
    {
        HideDialogue();
        Playing = true;
        TextPanel.SetActive(true);
        _playingDialog = dialog;
        PlayNextReplic();
    }

    private void PlayNextReplic()
    {
        _phraseId++;
        if (_phraseId >= _playingDialog.Phrases.Count)
        {
            HideDialogue();
        }
        else
        {
            PlayReplic(_playingDialog.Phrases[_phraseId]);
        }
    }

    private void HideDialogue()
    {
        Playing = false;
        _phraseId = 0;
        _playingDialog = null;
        TextPanel.gameObject.SetActive(false);
        LeftPerson.SetActive(false);
        RightPerson.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (_playingDialog != null && Typewriter.Completed)
            {
                PlayNextReplic();
            }
        }
    }

    private void PlayReplic(DialoguePhrase dialoguePhrase)
    {
        LeftPerson.SetActive(dialoguePhrase.Side == DialoguePhrase.PortraitSide.Left);
        RightPerson.SetActive(dialoguePhrase.Side == DialoguePhrase.PortraitSide.Right);

        switch (dialoguePhrase.Side)
        {
            case DialoguePhrase.PortraitSide.Left:
                LeftPortrait.gameObject.SetActive(dialoguePhrase.Char!=null);
                LeftName.gameObject.transform.parent.gameObject.SetActive(dialoguePhrase.Char!=null);
                if (dialoguePhrase.Char)
                {
                    LeftPortrait.sprite = dialoguePhrase.Char.Portrait;
                    LeftName.text = dialoguePhrase.Char.CharacterName.Text;
                }              
                break;
            case DialoguePhrase.PortraitSide.Right:
                RightPortrait.gameObject.SetActive(dialoguePhrase.Char != null);
                RightName.gameObject.transform.parent.gameObject.SetActive(dialoguePhrase.Char != null);
                if (dialoguePhrase.Char)
                {
                    RightPortrait.sprite = dialoguePhrase.Char.Portrait;
                    RightName.text = dialoguePhrase.Char.CharacterName.Text;
                }
                break;
        }

       // Typewriter.initialText = dialoguePhrase.Text.Text;
        Typewriter.Write(dialoguePhrase.Text.Text, new string[] { });
       // Typewriter.Write();
    }
}
