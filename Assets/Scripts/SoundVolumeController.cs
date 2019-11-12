using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolumeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DefaultRessources.OnSoundVolumeChanged += VolumeChanged;
        VolumeChanged(DefaultRessources.SoundVolume);
    }

    private void VolumeChanged(float v)
    {
        GetComponent<AudioSource>().volume = v;
    }
}
