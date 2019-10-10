using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClipPair Clip;

    public void Play()
    {
        MusicSource.Instance.Play(Clip);
    }

    public void SetVolume(float v)
    {
        MusicSource.Instance.SetVolume(v);
    }
}
