using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobVision : MonoBehaviour
{
    public Action<GameObject> OnInside, OnOutside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>()!=null)
        {
            OnInside(other.GetComponent<PlayerIdentity>().gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>() != null)
        {
            OnOutside(other.GetComponent<PlayerIdentity>().gameObject);
        }
    }
}
