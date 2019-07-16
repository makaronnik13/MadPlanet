using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public GameObject gameobgectmy;
    public Vector3 EButtonOffset;
    public UnityEvent OnActivate;
    public float Delay = 0f;
    public float DecreaseMultiplyer = 2;

    public void Activate()
    {
        OnActivate.Invoke();
    }
    public void ActivateGM()
    {
        gameobgectmy.SetActive(true);

    }
}
