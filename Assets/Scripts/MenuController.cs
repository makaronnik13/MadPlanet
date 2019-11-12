﻿using Assets.SimpleLocalization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Slider MusicVolume, SoundsVolume;

    [SerializeField]
    private GameObject SettingsPanel;

    [SerializeField]
    private TMPro.TextMeshProUGUI LangTitle;

    private bool SettingsShowing
    {
        get
        {
            return SettingsPanel.activeInHierarchy;
        }
    }

    [SerializeField]
    private AudioClipPair FocusSound, ClickSound;

    [SerializeField]
    private string SceneName;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private ParticleSystem Particles;

    [SerializeField]
    private Button ContinueBtn;

    private AsyncOperation _loadingOp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {

                Game.Instance.Paused.SetState(!Game.Instance.Paused.State);
            }

            if (SettingsShowing)
            {
                ToggleSettings();
            }
        }
    }

    private void Start()
    {
        MusicVolume.onValueChanged.AddListener(MusicVolumeChanged);
        SoundsVolume.onValueChanged.AddListener(SoundVolumeChanged);
        ContinueBtn.interactable = Game.Instance.gameData != null;
        Game.Instance.Paused.AddListener(OnPause, true);
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            b.onClick.AddListener(Click);
        }
        foreach (EventTrigger t in GetComponentsInChildren<EventTrigger>())
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { Focus(); });
            t.triggers.Add(entry);
        }
    }

    private void SoundVolumeChanged(float v)
    {
        DefaultRessources.SoundVolume = v;
    }

    private void MusicVolumeChanged(float v)
    {
        DefaultRessources.MusicVolume = v;
    }

    private void Focus()
    {
        SoundsPlayer.Instance.PlaySound(FocusSound);
    }

    private void Click()
    {
        SoundsPlayer.Instance.PlaySound(ClickSound);
    }

    private void OnPause(bool v)
    {
        Animator.SetBool("Paused", v);
    }

    public void StartNewGame(bool errase)
    {
        if (errase)
        {
            Game.Instance.gameData = new GameData();
            Game.Instance.Save();
            MusicSource.Instance.Play(Game.Instance.gameData.CurrentTrackId);
        }
        else
        {
            Debug.Log(Game.Instance.Paused.State);
            if (Game.Instance.Paused.State)
            {              
                Game.Instance.Paused.SetState(false);
                return;                
            }
            else
            {
                Game.Instance.Load();
                MusicSource.Instance.Play(Game.Instance.gameData.CurrentTrackId);
            }
        }

        Animator.SetTrigger("Loading");
        Particles.Stop();
        _loadingOp = SceneManager.LoadSceneAsync(SceneName);
        _loadingOp.completed += OnComplete;

    }

    private void OnComplete(AsyncOperation v)
    {
        StartCoroutine(FinishLoading());
    }

    private IEnumerator FinishLoading()
    {
        Game.Instance.Paused.SetState(false);
        MusicSource.Instance.OnPause(false);
        yield return new WaitForSeconds(1);
        Animator.SetTrigger("Loaded");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleSettings()
    {
        MusicVolume.value = DefaultRessources.MusicVolume;
        SoundsVolume.value = DefaultRessources.SoundVolume;
        LangTitle.text = LocalizationManager.Language.ToString();
        SettingsPanel.SetActive(!SettingsPanel.activeInHierarchy);
    }

    public void ChangeLanguage()
    {
        if (LocalizationManager.Language!=SystemLanguage.Russian)
        {
            LocalizationManager.Language = SystemLanguage.Russian;
        }
        else
        {
            LocalizationManager.Language = SystemLanguage.English;
        }

        LangTitle.text = LocalizationManager.Language.ToString();
    }
}
