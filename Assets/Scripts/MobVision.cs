using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobVision : MonoBehaviour
{
    public Action<PlayerIdentity> OnInside = (p) => { };
    public Action<PlayerIdentity> OnOutside = (p) => { };

    private void OnTriggerEnter(Collider other)
    {
        PlayerIdentity pi = other.GetComponent<PlayerIdentity>();
        if (pi!=null)
        {
            OnInside(pi);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>() != null)
        {
            OnOutside(other.GetComponent<PlayerIdentity>());
        }
    }
}
