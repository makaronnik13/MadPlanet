using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorController : MonoBehaviour
{
    public AudioClipPair Sound;

    public bool Locked = false;
    private bool _open = false;
    public bool Open
    {
        get
        {
            return _open;
        }
        set
        {
            if (!Locked)
            {
                SoundsPlayer.Instance.PlaySound(Sound);
                _open = value;
                GetComponent<Animator>().SetBool("Open", _open);
            }
        }
    }


    public void SetLock(bool v)
    {
        Locked = v;
    }

    public void DelayOpen(float delay)
    {
        StartCoroutine(Delay(delay, true));
    }

    private IEnumerator Delay(float delay, bool v)
    {
        yield return new WaitForSeconds(delay);
        SetState(v);
    }

    public void SetState(bool v)
    {
        Open = v;
    }
}
