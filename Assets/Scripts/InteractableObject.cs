using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets._2D;

public class InteractableObject : MonoBehaviour
{
    public enum InteractionMode
    {
        Normal,
        Keys
    }

    public InteractionMode IneractionMode = InteractionMode.Normal;

    [SerializeField]
    private bool active = true;
    public Vector3 EButtonOffset;
    public UnityEvent OnActivate;
    public float Delay = 0f;
    public float DecreaseMultiplyer = 2;
    public bool SaveAfterUse = false;


    public bool Active
    {
        get
        {
            return active;
        }
        set
        {
            active = value;
            if (active == false)
            {
                foreach (InteractionModule im in FindObjectsOfType<InteractionModule>())
                {
                    im.interactableObject = null;
                }
            }
            else
            {
                foreach (InteractionModule im in FindObjectsOfType<InteractionModule>())
                {
                    if (GetComponent<SphereCollider>()!=null)
                    {
                        if (Vector3.Distance(transform.position, im.transform.position) < GetComponent<SphereCollider>().radius * 2f && !im.GetComponent<PlatformerCharacter2D>().Grabed)
                        {
                            im.interactableObject = this;
                        }
                    }
                }
            }
        }
    }

    public void Activate()
    {
        OnActivate.Invoke();
        if (SaveAfterUse)
        {
            Game.Instance.Save();
        }
    }

}
