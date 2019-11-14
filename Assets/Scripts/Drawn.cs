using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets._2D;

public class Drawn : MonoBehaviour
{
   [SerializeField]
   PlatformerCharacter2D Char;

    public AudioSource DrawnSoundSource;

    [SerializeField]
    public PostProcessProfile Vingete;

    [SerializeField]
    public SpriteRenderer Fade;

    private void Start()
    {
        Char.DrawnRateChanged += RateChanged;
        Char.DieRateChanged += GrabRateChanged;
    }

    private void GrabRateChanged(float v)
    {
        Vingete.GetSetting<Vignette>().intensity.value = v;
        Fade.color = Color.Lerp(Color.black * 0, Color.black, v);
    }

    private void RateChanged(float v)
    {
        if (v>0 && UnityEngine.Random.value<0.2f)
        {
            GetComponent<ParticleSystem>().Emit(1);
        }

        Vingete.GetSetting<Vignette>().intensity.value = v;

        Fade.color = Color.Lerp(Color.black*0, Color.black, v);

        if (v == 0)
        {
            DrawnSoundSource.volume = 0;
        }
        else
        {
            DrawnSoundSource.volume += 0.03f;
        }
    }

    private void OnDestroy()
    {
        Vingete.GetSetting<Vignette>().intensity.value = 0;
    }

    
}
