﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public AudioSource SoundSource;

    public AudioClip Presound, ExplosionSound;

    public ParticleSystem Crack, Spike;
    public GameObject Colliders;
    public bool Shaking = false;
    public float Lifetime = 5f;

    public void Shake(float delay)
    {
        SoundSource.PlayOneShot(Presound);
        Crack.Play();
        StartCoroutine(Damage(delay));
    }

    private IEnumerator Damage(float t)
    {
        SoundSource.PlayOneShot(ExplosionSound);
        Spike.startLifetime = Lifetime;
        Shaking = true;
        yield return new WaitForSeconds(t);
        Spike.Play();
        Colliders.SetActive(true);
        Crack.Stop();
        yield return new WaitForSeconds(Lifetime);
        Spike.Stop();
        Colliders.SetActive(false);
        Shaking = false;
    }
}
