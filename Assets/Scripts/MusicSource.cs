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

    public AudioClipPair MainMenuTheme;


    public List<AudioClipPair> OSTS;

    private Queue<AudioClipPair> _tracksQueqe;

    private AudioSource _source;
    private AudioSource source
    {
        get
        {
            if (_source == null)
            {
                _source = GetComponent<AudioSource>();
            }
            return _source;
        }
    }

    private AudioClipPair _currentTrack;
    private AudioClipPair currentTrack
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

        if (source.clip)
        {
            while (ChangeTime > 0)
            {
                source.volume = (ChangeTime / (t / 2f)) * DefaultRessources.MusicVolume;
                ChangeTime -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        source.clip = _currentTrack.Clip;
        source.outputAudioMixerGroup = _currentTrack.Group;
        source.Play();

        ChangeTime = t/2f;
        while (ChangeTime>0)
        {
            source.volume = (1-(ChangeTime / (t/2f))) * DefaultRessources.MusicVolume;
            ChangeTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void Start()
    {
        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(LoopTracks(new List<AudioClipPair>() { MainMenuTheme}));
        DefaultRessources.OnMusicVolumeChanged += VolumeChanged;
    }

    private void VolumeChanged(float v)
    {
        source.volume = v;
    }


    public void SetVolume(float v)
    {
        currentVolume = v;
        GetComponent<AudioSource>().volume = DefaultRessources.MusicVolume * v;
    }

    public void Play(AudioClipPair clip)
    {
        if (_loop != null)
        {
            swithTime = 0;
            StopCoroutine(_loop);
            _loop = null;
        }
        _loop = StartCoroutine(LoopTracks(new List<AudioClipPair>() {clip}));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_loop!=null)
        {
            swithTime = 0;
            StopCoroutine(_loop);
            _loop = null;
        }

        if (scene.name == "Menu_Test")
        {
            _loop = StartCoroutine(LoopTracks(new List<AudioClipPair>() { MainMenuTheme }));
        }

        if (scene.name == "Scene_1_21_09")
        {
            _loop = StartCoroutine(LoopTracks(OSTS));
        }
    }

    private IEnumerator LoopTracks(List<AudioClipPair> clips)
    {
        _tracksQueqe = new Queue<AudioClipPair>(clips.OrderBy(o => Guid.NewGuid()));
        while (true)
        {
            currentTrack = _tracksQueqe.Dequeue();
            _tracksQueqe.Enqueue(currentTrack);
            length = currentTrack.Clip.length;
            swithTime = currentTrack.Clip.length - CHANGE_TIME / 2f;
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
