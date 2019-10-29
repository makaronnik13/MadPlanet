using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSource : MonoBehaviour
{
    public static MusicSource Instance;
    private Coroutine _loop;
    public float swithTime;
    private float length = 0;
    private float currentVolume = 1;

    private static float CHANGE_TIME = 1f;

    private Queue<AudioClip> _tracksQueqe;

    public List<AudioClip> Clips = new List<AudioClip>();

    public AudioSource Music, Menu;

    private AudioClip _currentTrack;
    private AudioClip currentTrack
    {
        get
        {
            return _currentTrack;
        }
        set
        {
            StopCoroutine(ChangeTrack(CHANGE_TIME));
            _currentTrack = value;
            StartCoroutine(ChangeTrack(CHANGE_TIME));
        }
    }


    private IEnumerator ChangeTrack(float t)
    {
        float ChangeTime = t/2f;

        if (Music.clip)
        {
            while (ChangeTime > 0)
            {
                Music.volume = (ChangeTime / (t / 2f)) * DefaultRessources.MusicVolume;
                ChangeTime -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        Music.clip = _currentTrack;
        Music.Play();

        ChangeTime = t/2f;
        while (ChangeTime>0)
        {
            Music.volume = (1-(ChangeTime / (t/2f))) * DefaultRessources.MusicVolume;
            ChangeTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void Start()
    {
        Instance = this;
        //SceneManager.sceneLoaded += OnSceneLoaded;
        DefaultRessources.OnMusicVolumeChanged += VolumeChanged;
        Game.Instance.Paused.AddListener(OnPause);
        OnPause(true);
    }

    public void OnPause(bool v)
    {
        Debug.Log("ON PAUSE "+v);
        StartCoroutine(SetMusicMode(v));
    }

    private IEnumerator SetMusicMode(bool v)
    {
        Debug.Log(v+"!!!");
        float t = 1f;
        float music = 0, menu = 0;
        if (v)
        {
            menu = 1f;
        }
        else
        {
            music = 1f;
        }

        float startMusicVolume = 0, startMenuVolume = 0;

        Music.outputAudioMixerGroup.audioMixer.GetFloat("MusicVol", out startMusicVolume);
        Menu.outputAudioMixerGroup.audioMixer.GetFloat("MenuVol", out startMenuVolume);

        startMusicVolume = (startMusicVolume+80f)/100f;
        startMenuVolume = (startMenuVolume+80f) / 100f;

        float musicMasterLevel, menuMasterLevel;

        while (t>0)
        {
            musicMasterLevel = Mathf.Lerp(startMusicVolume, music, 1 / t);
            menuMasterLevel = Mathf.Lerp(startMenuVolume, menu, 1 / t);

            Music.outputAudioMixerGroup.audioMixer.SetFloat("MusicVol", musicMasterLevel*100f-80f);
            Menu.outputAudioMixerGroup.audioMixer.SetFloat("MenuVol", menuMasterLevel * 100f - 80f);
            t -= Time.deltaTime;
            yield return null;
        }
    }

    private void VolumeChanged(float v)
    {
        Music.volume = v;
        Menu.volume = v;
    }


    public void SetVolume(float v)
    {
        currentVolume = v;
        Music.outputAudioMixerGroup.audioMixer.SetFloat("MusicVol", v*100f-80f);
    }

    public void Play(int id)
    {
        Debug.Log("Play");
        SetMusicMode(false);
        Game.Instance.gameData.CurrentTrackId = id;
        if (_loop != null)
        {
            swithTime = 0;
            StopCoroutine(_loop);
            _loop = null;
        }
        _loop = StartCoroutine(LoopTracks(new List<AudioClip>() {Clips[id]}));
    }

    /*
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_loop!=null)
        {
            swithTime = 0;
            StopCoroutine(_loop);
            _loop = null;
        }
    }*/

    private IEnumerator LoopTracks(List<AudioClip> clips)
    {
        _tracksQueqe = new Queue<AudioClip>(clips.OrderBy(o => Guid.NewGuid()));
        while (true)
        {
            currentTrack = _tracksQueqe.Dequeue();
            _tracksQueqe.Enqueue(currentTrack);
            length = currentTrack.length;
            swithTime = currentTrack.length - CHANGE_TIME / 2f;
            
            while (swithTime>0)
            {  
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void Update()
    {
        swithTime -= Time.deltaTime;
    }
}
