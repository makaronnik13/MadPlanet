using com.armatur.common.serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [Savable]
    [SerializeCollection("Quests", FieldRequired.False)]
    public List<QuestInstance> Quests = new List<QuestInstance>();

    [Savable]
    [SerializeData("SavePointX", FieldRequired.False)]
    public float SavepointX = 0;

    [Savable]
    [SerializeData("SavePointY", FieldRequired.False)]
    public float SavepointY = 0;

    [Savable]
    [SerializeData("SavePointZ", FieldRequired.False)]
    public float SavepointZ = 0;

    [Savable]
    [SerializeData("Skin", FieldRequired.False)]
    public int Skin = 0;

    [Savable]
    [SerializeData("MusicTrackId", FieldRequired.False)]
    private int currentTrackId = 0;

    public int CurrentTrackId
    {
        get
        {
            return currentTrackId;
        }
        set
        {
            currentTrackId = value;
            Game.Instance.Save();
        }
    }


    public GameData()
    {

    }

    public Vector3 Position
    {
        get
        {
            return new Vector3(SavepointX, SavepointY, SavepointZ);
        }
        set
        {
            SavepointX = value.x;
            SavepointY = value.y;
            SavepointZ = value.z;
        }
    }
}