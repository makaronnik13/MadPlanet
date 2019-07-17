using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHolder : MonoBehaviour
{
    public AudioClipPair Clip;

    public void Play()
    {
        SoundsPlayer.Instance.PlaySound(Clip);
    }
}
