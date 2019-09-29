using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DitzelGames.FastIK;

public class Boss : MonoBehaviour
{
    public GameObject SpikePrefab, SplitPrefab;
    public Animator Animator;
    public float SplitForce;
    public float SpikesTime = 15f, TentaclesTime = 15;
    public float NearSpikesRate = 1f;
    public float FarSpikesRate = 2f;
    private bool spiking = false;
    public float spikeDamageDelay = 4f;
    public float playerDangerRadius = 3;
    public float bossDangeRadius = 5;


    [ContextMenu("StartAttack")]
    public void StartAttack()
    {
        StartCoroutine(TentackleTime(TentaclesTime));
    }

    private IEnumerator TentackleTime(float tentaclesTime)
    {
        Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(tentaclesTime);
        StartCoroutine(SpikeTime(SpikesTime));
    }

    private IEnumerator SpikeTime(float spikesTime)
    {
        Animator.SetBool("Spikes", true);
        spiking = true;
        StartCoroutine(NearSpikesTime());
        StartCoroutine(FarSpikesTime());
        yield return new WaitForSeconds(spikesTime);
        spiking = false;
        StartCoroutine(TentackleTime(TentaclesTime));
        Animator.SetBool("Spikes", false);
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
    
    [ContextMenu("Fire")]
    public void TestBullet()
    {
        LaunchBullet(0);
    }

    public void LaunchBullet(int armId)
    {
        Vector3 pos = GetComponentsInChildren<FastIKFabric>()[armId].transform.position;
        GameObject newBullet = Instantiate(SplitPrefab);
        newBullet.transform.position = pos;
        Vector3 playerPos = FindObjectOfType<PlayerIdentity>().transform.position;

        newBullet.GetComponent<Bullet>().Fly((playerPos - pos) * SplitForce, playerPos);           
    }

}
