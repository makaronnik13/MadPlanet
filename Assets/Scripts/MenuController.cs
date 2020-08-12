using Assets.SimpleLocalization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject Reset;

    [SerializeField]
    private Slider MusicVolume, SoundsVolume;

    [SerializeField]
    private GameObject SettingsPanel;

    [SerializeField]
    private TMPro.TMP_Dropdown Lang;

    [SerializeField]
    private GameObject LoadingPanel;

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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
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

        Lang.onValueChanged.AddListener(ChangeLang);
    }

    private void ChangeLang(int v)
    {
        switch (v)
        {
            case 0:
                LocalizationManager.Language = SystemLanguage.Russian;
                break;
            case 1:
                LocalizationManager.Language = SystemLanguage.English;
                break;
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
        LoadingPanel.SetActive(true);
        _loadingOp = SceneManager.LoadSceneAsync(SceneName);
        _loadingOp.completed += OnComplete;

    }

    private void OnComplete(AsyncOperation v)
    {
        LoadingPanel.SetActive(true);
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
        Reset.SetActive(Game.Instance.Paused.State);
        MusicVolume.value = DefaultRessources.MusicVolume;
        SoundsVolume.value = DefaultRessources.SoundVolume;
        if (LocalizationManager.Language == SystemLanguage.English)
        {
            Lang.value = 1;
        }
        else
        {
            Lang.value = 0;
        }
        SettingsPanel.SetActive(!SettingsPanel.activeInHierarchy);
    }

    public void ResetScene()
    {
        Animator.SetTrigger("Loading");
        Particles.Stop();
        SceneManager.UnloadSceneAsync(SceneName);
        _loadingOp = SceneManager.LoadSceneAsync(SceneName);
        ToggleSettings();
        Game.Instance.Paused.SetState(false);
    }

}
