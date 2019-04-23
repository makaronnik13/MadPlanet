using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerObject : MonoBehaviour
{
    public Animator DangerAnimator;
    public float DangerDelay;
    public float DangerRadius = 3;
    public float ActivationDelay = 3f;

    private void Start()
    {
        CreateDanger();
    }

    [ContextMenu("Danger!")]
    public void CreateDanger()
    {
        DangerAnimator.speed = 1f/DangerDelay;
        DangerAnimator.SetTrigger("Active");
    }

    public void ActivateDanger()
    {
        DangerAnimator.speed = 1f;
        DangerAnimator.SetTrigger("Damage");
    }

    public void DamagePlayer()
    {
        GetComponent<CubeEmmiter>().Emmit();
        Collider[] allOverlappingColliders = Physics.OverlapSphere(transform.position, DangerRadius);
        foreach (Collider c in allOverlappingColliders)
        {
            if (c.GetComponent<PlayerIdentity>())
            {
                c.GetComponent<PlayerIdentity>().Damage(transform.position, DangerRadius);
            }
        }
    }

    public void ResetDanger()
    {
        StartCoroutine(DelayedDanger(ActivationDelay));
    }

    private IEnumerator DelayedDanger(float activationDelay)
    {
        yield return new WaitForSeconds(activationDelay);
        CreateDanger();
    }
}
