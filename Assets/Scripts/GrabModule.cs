using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class GrabModule : MonoBehaviour
{
    public float GrabTime = 3;
    public InteractableObject Obj;

    public bool Active
    {
        get
        {
            return Obj.Active;
        }
        set
        {
            Obj.Active = value;
        }
    }

    public void SetGrab(bool v)
    {
        Active = v;
        FindObjectOfType<PlayerIdentity>().GetComponent<PlatformerCharacter2D>().Grab(v, GrabTime);
    }
}
