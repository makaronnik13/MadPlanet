using DitzelGames.FastIK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallsThrower : MonoBehaviour
{
    public GameObject SplitPrefab;
    public float SplitForce;
    private Dictionary<int, GameObject> launchingBullets = new Dictionary<int, GameObject>();

    public void CreateBullet(int armId)
    {
        GameObject newBullet = Instantiate(SplitPrefab);
        newBullet.transform.SetParent(GetComponentsInChildren<FastIKFabric>()[armId].transform.GetChild(1));
        newBullet.transform.localPosition = Vector3.zero;
        if (launchingBullets.ContainsKey(armId))
        {
            launchingBullets.Remove(armId);
        }
        launchingBullets.Add(armId, newBullet);
    }

    public void LaunchBullet(int armId)
    {
        if (launchingBullets.ContainsKey(armId))
        {

            Vector3 pos = GetComponentsInChildren<FastIKFabric>()[armId].transform.GetChild(1).position;
            GameObject blt = launchingBullets[armId];
            if (blt != null)
            {
                blt.transform.SetParent(null);
                Vector3 playerPos = FindObjectOfType<PlayerIdentity>().transform.position + Vector3.up * 2f;
                blt.GetComponent<Bullet>().Fly((playerPos - pos) * SplitForce, playerPos);
            }
            launchingBullets.Remove(armId);
        }
    }

    public void Explode()
    {
        foreach (KeyValuePair<int, GameObject> pair in launchingBullets)
        {
            if (pair.Value)
            {
                pair.Value.GetComponent<Bullet>().Explode();
            }
            
        }
        launchingBullets.Clear();
    }
}
