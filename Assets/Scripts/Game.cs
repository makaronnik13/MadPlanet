using com.armatur.common.flags;
using com.armatur.common.serialization;
using com.armatur.common.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class Game : MonoBehaviour
{
    [Savable]
    [SerializeData("GameData", FieldRequired.False)]
    public GameData gameData = null;

    public static Game Instance;
    public Action<QuestInstance> OnQuestTaken = (q) => { };
    public GenericFlag<float> Noise = new GenericFlag<float>("Noise", 0);

    private PlatformerCharacter2D _controller;
    public PlatformerCharacter2D Controller
    {
        get
        {
            if (_controller == null)
            {
                PlayerIdentity identity = FindObjectOfType<PlayerIdentity>();
                if (identity)
                {
                    _controller = identity.GetComponent<PlatformerCharacter2D>();

                    _controller.DrawnRateChanged += DrawnChanged;
                    _controller.GrabRateChanged += DrawnChanged;
                    if (_controller && gameData.Position != Vector3.zero)
                    {
                        _controller.transform.position = gameData.Position;
                    }
                }
            }
            return _controller;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        Load();

        _controller = Controller;

        if (Controller && gameData.Position != Vector3.zero)
        {
            Debug.Log("set pos: " + gameData.Position);
            Controller.transform.position = gameData.Position;
        }
    }

    [ContextMenu("Errase")]
    public void ErraseProfile()
    {
        Debug.Log("Errase");
        if (File.Exists(GameSessionSaveFolder+GameSessionSaveFilename))
        {
            File.Delete(GameSessionSaveFolder + GameSessionSaveFilename);
            gameData = new GameData();
        }
    }

    private string GameSessionSaveFolder
    {
        get
        {
            return string.Format("{0}/", Application.persistentDataPath);
        }
    }

    private string GameSessionSaveFilename
    {
        get
        {
            return "gamesession.xml";
        }
    }

    public void Load()
    {
        Debug.Log("Load");
        MiscUtil.TryLoadSavedState(gameData, GameSessionSaveFolder, GameSessionSaveFilename, true);
    }

    private void Awake()
    {
        Instance = this;
        gameData = new GameData();
    }



    private void DrawnChanged(float v)
    {
        if (v>=1)
        {
            Debug.Log("restart");
            Restart();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void TakeQuest(Quest quest)
    {
        if (gameData.Quests.FirstOrDefault(q=>q.Quest == quest)==null)
        {
            QuestInstance newQuest = new QuestInstance(quest);
            gameData.Quests.Add(newQuest);
            OnQuestTaken(newQuest);
            Save();
        }
    }

    public void Save()
    {
        if (Controller)
        {
            gameData.Position = Controller.transform.position;
        }
        
        MiscUtil.SaveState(gameData, GameSessionSaveFolder, GameSessionSaveFilename, true);
    }
}
