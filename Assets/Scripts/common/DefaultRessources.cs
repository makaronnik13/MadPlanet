using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DefaultRessources
{
    public static float SoundVolume = 1;

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
