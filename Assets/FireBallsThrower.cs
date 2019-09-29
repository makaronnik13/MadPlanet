﻿using DitzelGames.FastIK;
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
        newBullet.transform.SetParent(GetComponentsInChildren<FastIKFabric>()[armId].transform);
        newBullet.transform.localPosition = Vector3.zero;
        launchingBullets.Add(armId, newBullet);
    }

    public void LaunchBullet(int armId)
    {
        Vector3 pos = GetComponentsInChildren<FastIKFabric>()[armId].transform.position;
        GameObject blt = launchingBullets[armId];
        blt.transform.SetParent(null);
        launchingBullets.Remove(armId);
        Vector3 playerPos = FindObjectOfType<PlayerIdentity>().transform.position;
        blt.GetComponent<Bullet>().Fly((playerPos - pos) * SplitForce, playerPos);
    }
}