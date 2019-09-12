using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {
            other.GetComponent<PlayerIdentity>().Die();
        }
    }

    private void Start()
    {
        Destroy(gameObject, 2f);
        Destroy(this, 0.5f);
    }
}
