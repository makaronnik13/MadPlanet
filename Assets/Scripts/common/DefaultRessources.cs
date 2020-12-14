using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DefaultRessources
{
    public static float SoundVolume = 0.5f;

    public static Action<float> OnMusicVolumeChanged = (f) => { };
    public static float MusicVolume
    {
        get
        {
            if (!PlayerPrefs.HasKey("MusicVolume"))
            {
                MusicVolume = 0.5f;
            }
            return PlayerPrefs.GetFloat("MusicVolume");
        }
        set
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            OnMusicVolumeChanged(MusicVolume);
        }
    }

    private static Quest[] _quests;
    public static Quest[] Quests
    {
        get
        {
            if (_quests == null)
            {
                _quests = Resources.LoadAll<Quest>("Quests");
            }
            return _quests;
        }
    }

    public static Action<float> OnSoundVolumeChanged { get; internal set; }

    public static Quest GetQuest(int questId)
    {
        return Quests[questId];
    }
}
