using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField]
    private Transform Bullet;

    [SerializeField]
    private Transform Shadow;

    private float startDist;

    private void Start()
    {
        startDist = Vector3.Distance(Shadow.position, Bullet.position);
    }

    private void Update()
    {
        float dist = Vector3.Distance(Shadow.position, Bullet.position);
        Shadow.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, 1f - dist/startDist);
    }
}
