using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootEmitter : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem LeftFoot, RightFoot;

    [SerializeField]
    private AudioSource source;

    public bool EmmitionEnabled = true;

    public void Left()
    {
        if (source)
        {
            source.PlayOneShot(source.clip);
        }
        if (!EmmitionEnabled)
        {
            return;
        }
        LeftFoot.Play();
    }

    public void Right()
    {
        if (source)
        {
            source.PlayOneShot(source.clip);
        }
        if (!EmmitionEnabled)
        {
            return;
        }
        RightFoot.Play();
    }
}
