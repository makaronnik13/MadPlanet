using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private bool active = true;
    public Vector3 EButtonOffset;
    public UnityEvent OnActivate;
    public float Delay = 0f;
    public float DecreaseMultiplyer = 2;

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
                    Debug.Log(Vector3.Distance(transform.position, im.transform.position)+"/"+ GetComponent<SphereCollider>().radius);
                    if (Vector3.Distance(transform.position, im.transform.position)<GetComponent<SphereCollider>().radius*2f)
                    {
                        im.interactableObject = this;
                    }
                }
            }
        }
    }

    public void Activate()
    {
        OnActivate.Invoke();
        Game.Instance.Save();
    }

}
