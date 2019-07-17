using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Trigger : MonoBehaviour
{
    public UnityEvent OnEnter, OnExit, OnStay;
    public bool DestroyAfterActivation = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {
            OnEnter.Invoke();
            if (DestroyAfterActivation)
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {
            OnExit.Invoke();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {
            OnStay.Invoke();
        }
    }

}
