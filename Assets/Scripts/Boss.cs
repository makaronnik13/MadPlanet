using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DitzelGames.FastIK;

public class Boss : MonoBehaviour
{

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
            }
        }
    }

    public MobVision Vision;
    public GameObject SpikePrefab;
    public Animator Animator;
    public float SpikesTime = 15f, BulletsTime = 15;
    public float NearSpikesRate = 1f;
    public float BulletsRate = 1f;
    public float FarSpikesRate = 2f;
    private bool spiking = false;
    public float spikeDamageDelay = 4f;
    public float playerDangerRadius = 3;
    public float bossDangeRadius = 5;
    public int Lifes = 10;


    public void StartAttack()
    {
        Vision.OnInside += OnPlayerInside;
        Vision.OnOutside += OnplayerOutside;
        StartCoroutine(SwitchMode());
        AType = AtackType.Far;
    }

    private IEnumerator SwitchMode()
    {
        while (true)
        {
            yield return null;
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
        Debug.Log("SPIKE TIME");
        Animator.SetBool("Spikes", true);
        spiking = true;
        StartCoroutine(NearSpikesTime());
        StartCoroutine(FarSpikesTime());
        yield return new WaitForSeconds(SpikesTime);
        spiking = false;
        StartCoroutine(BulletTime(BulletsTime));
        Animator.SetBool("Spikes", false);
    }

    private IEnumerator BulletTime(float t)
    {
        Animator.SetBool("Firing", true);
        yield return new WaitForSeconds(t);
        Animator.SetBool("Firing", false);
    }

    private IEnumerator FarSpikesTime()
    {
        while (spiking)
        {
            ActivateRandomSpike(FindObjectOfType<PlayerIdentity>().transform, playerDangerRadius);
            yield return new WaitForSeconds(FarSpikesRate);
        }
    }

    private IEnumerator NearSpikesTime()
    {
        while (spiking)
        {
            ActivateRandomSpike(transform, bossDangeRadius);
            yield return new WaitForSeconds(NearSpikesRate);
        }
    }

    private void ActivateRandomSpike(Transform aim, float offsetRadius)
    {
        Vector3 pos = aim.transform.position;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(new Ray(aim.transform.position, Vector3.down), out hit, 5))
        {
            pos = hit.point;
        }
        Vector3 offset = new Vector3(UnityEngine.Random.Range(-1f, 1f),0, UnityEngine.Random.Range(-1f, 1f)).normalized*offsetRadius;
        pos += offset;

        GameObject newSpike = Instantiate(SpikePrefab);
        newSpike.transform.position = pos;
        Destroy(newSpike, spikeDamageDelay*2f);
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
        Destroy(gameObject);  
    }
}
