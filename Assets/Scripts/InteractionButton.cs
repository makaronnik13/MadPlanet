﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    public InteractionModule Module;
    public Animator Animator;
    public Image FillImg;

    // Start is called before the first frame update
    void Start()
    {
        Module.OnObjectChanged += InteractableObjectChanged;
        Module.OnFillChanged += FillChanged;
    }

    private void FillChanged(float val, float max)
    {
        FillImg.gameObject.SetActive(max>0);
        FillImg.fillAmount = (val+0.0f) / max;
    }

    private void OnDestroy()
    {
        Module.OnObjectChanged -= InteractableObjectChanged;
        Module.OnFillChanged += FillChanged;
    }

    private void InteractableObjectChanged(InteractableObject obj)
    {
        if (obj)
        {
            Animator.SetBool("Showing", true);
            transform.position = obj.transform.position + obj.EButtonOffset;
        }
        else
        {
            Animator.SetBool("Showing", false);
        }
    }
}
