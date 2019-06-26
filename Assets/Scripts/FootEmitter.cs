using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootEmitter : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem LeftFoot, RightFoot;

    public bool EmmitionEnabled = true;

    public void Left()
    {
        if (!EmmitionEnabled)
        {
            return;
        }
        LeftFoot.Play();
    }

    public void Right()
    {
        if (!EmmitionEnabled)
        {
            return;
        }
        RightFoot.Play();
    }
}
