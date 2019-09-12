﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerMountain : MonoBehaviour
{
    public enum SplitMode
    {
        Bullets,
        Zones
    }

    public float AttaksDellay = 6;
    [SerializeField]
    private float SplitForce;
    [SerializeField]
    private MobVision Vision;
    [SerializeField]
    private SplitMode Mode = SplitMode.Bullets;
    [SerializeField]
    private Transform BulletsSource;

    public GameObject SplitPrefab, DangerZonePrefab;

    private PlayerIdentity aim;

    private void Start()
    {
        StartCoroutine(Splitting());
        Vision.OnInside += OnInside;
        Vision.OnOutside += OnOutside;
    }

    private void OnOutside(PlayerIdentity obj)
    {
        aim = null;
    }

    private void OnInside(PlayerIdentity obj)
    {
        aim = obj;
    }

    private IEnumerator Splitting()
    {
        while (true)
        {
            TrySplit();
            yield return new WaitForSeconds(AttaksDellay);
        }
    }

    private void TrySplit()
    {
        if (aim)
        {
            GetComponent<Animator>().SetTrigger("Fire");
        }
    }

    public void Split()
    {
        GameObject newBullet = Instantiate(SplitPrefab);
        newBullet.transform.position = BulletsSource.transform.position;
        newBullet.GetComponent<Bullet>().Fly(Vector3.up * SplitForce, aim.transform.position);
    }
}
