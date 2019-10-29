using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private int ClipId;

    public void Play()
    {
        MusicSource.Instance.Play(ClipId);
    }

    public void SetVolume(float v)
    {
        MusicSource.Instance.SetVolume(v);
    }
}
