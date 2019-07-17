using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    private static SoundsPlayer _instance;
    public static SoundsPlayer Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<SoundsPlayer>();
            }
            return _instance;
        }
    }

    // Use this for initialization
    void Start ()
    {
        DefaultRessources.OnSoundVolumeChanged += VolumeChanged;
    }

    private void VolumeChanged(float v)
    {
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            source.volume = v;
        }
    }

    public void PlaySound(AudioClipPair pair)
    {
        AudioSource _source = gameObject.AddComponent<AudioSource>();
        _source.volume = DefaultRessources.SoundVolume;
        _source.outputAudioMixerGroup = pair.Group;
        _source.panStereo = 0;
        _source.PlayOneShot(pair.Clip);
        Destroy(_source, pair.Clip.length);
    }
}
