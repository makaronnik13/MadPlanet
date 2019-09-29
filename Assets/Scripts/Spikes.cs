using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public ParticleSystem Crack, Spike;
    public Collider Collider;
    public bool Shaking = false;


    public void Shake(float delay)
    {
        Crack.Play();
        StartCoroutine(Damage(delay));
    }

    private IEnumerator Damage(float t)
    {
        Shaking = true;
        yield return new WaitForSeconds(t);
        Spike.Play();
        Collider.enabled = true;
        Crack.Stop();
        yield return new WaitForSeconds(0.5f);
        Spike.Stop();
        Collider.enabled = false;
        Shaking = false;
    }
}
