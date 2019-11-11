using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DitzelGames.FastIK;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public UnityEvent OnDeath;

    private enum AtackType
    {
        Near,
        Far
    }

    private AtackType aType = AtackType.Far;
    private AtackType AType
    {
        get
        {
            return aType;
        }
        set
        {
            aType = value;
            if (InRage)
            {

            }
            else
            {
                if (aType == AtackType.Near)
                {
                    Animator.SetTrigger("Attack");
                    GetComponentInChildren<FireBallsThrower>().Explode();
                }
                else
                {
                    Animator.SetTrigger("Firing");
                }
                
            }
        }
    }

    private bool inRage = false;
    private bool InRage
    {
        get
        {
            return inRage;
        }
        set
        {
            inRage = value;
            if (InRage)
            {
                StartCoroutine(SpikeTime());
                StopCoroutine(SwitchMode());
            }
            else
            {
                StartCoroutine(SwitchMode());
            }
        }
    }

    public Transform NearSpikes, FarSpikes;
    private List<Vector3> spikesPositions = new List<Vector3>();

    public AudioSource Source;
    public AudioClip Appear, Death;

    public MobVision Vision;
    public GameObject SpikePrefab;
    public Animator Animator, BossAnimator;
    public float ModeSwitchTime = 15f;
    public float BulletsRate = 1f;
    public float FarSpikesRate = 2f;
    private bool spiking = false;
    public float spikeDamageDelay = 4f;
    public int Lifes = 10;

    public void StartAttack()
    {
        Source.PlayOneShot(Appear);
        Vision.OnInside += OnPlayerInside;
        Vision.OnOutside += OnplayerOutside;
        StartCoroutine(SwitchMode());
        AType = AtackType.Far;
    }

    private IEnumerator SwitchMode()
    {
        while (!InRage)
        {
            Debug.Log(ModeSwitchTime);
            yield return new WaitForSeconds(ModeSwitchTime);
            Debug.Log("spikes");
            InRage = true;
        }
    }

    private void OnplayerOutside(PlayerIdentity p)
    {
        AType = AtackType.Far;
    }

    private void OnPlayerInside(PlayerIdentity p)
    {
        Debug.Log(p);
        AType = AtackType.Near;
    }


    private IEnumerator SpikeTime()
    {
        yield return new WaitForSeconds(1f);
        Animator.SetTrigger("Spikes");
        spiking = true;
        foreach (Transform t in NearSpikes)
        {
            ActivateRandomSpike(t.position, 0);
        }
        StartCoroutine(FarSpikesTime());
        if (spiking)
        {
            yield return null;
        }
        InRage = false;
        AType = AtackType.Far;
    }

    private IEnumerator FarSpikesTime()
    {
        spikesPositions = new List<Vector3>();
        foreach (Transform t in FarSpikes)
        {
            spikesPositions.Add(t.position);
        }
        spikesPositions = spikesPositions.OrderBy(s=>Guid.NewGuid()).ToList();

        while (spikesPositions.Count>0)
        {
            Vector3 pos = spikesPositions.FirstOrDefault();
            ActivateRandomSpike(pos, 0);
            spikesPositions.Remove(pos);
            yield return new WaitForSeconds(FarSpikesRate);
        }
        spiking = false;
    }


    private void ActivateRandomSpike(Vector3 aim, float offsetRadius)
    {
        if (Game.Instance.Paused.State)
        {
            return;
        }
        Vector3 pos = aim;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(new Ray(aim, Vector3.down), out hit, 5, ~LayerMask.GetMask("Player")))
        {
            pos = hit.point;
        }
        Vector3 offset = new Vector3(UnityEngine.Random.Range(-1f, 1f),0, UnityEngine.Random.Range(-1f, 1f)).normalized*offsetRadius;
        pos += offset;

        GameObject newSpike = Instantiate(SpikePrefab);
        newSpike.transform.position = pos;
        Destroy(newSpike, spikeDamageDelay*10f);
        newSpike.GetComponent<Spikes>().Shake(spikeDamageDelay);       
    }
    
    public void Damage()
    {
        Lifes--;
        InRage = true;
        if (Lifes == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Source.PlayOneShot(Death);
        Animator.SetTrigger("Death");
        BossAnimator.SetTrigger("Dead");
        Destroy(gameObject, 2);
        OnDeath.Invoke();
    }
}
