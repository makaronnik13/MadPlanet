using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Platform : MonoBehaviour
{
    private Transform _platformedObject;
    private Vector3 _delta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {

            _platformedObject = other.transform;
            _platformedObject.GetComponent<PlatformerCharacter2D>().Platformed = true;
            _platformedObject.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {
            _platformedObject.SetParent(null);
            _platformedObject.GetComponent<PlatformerCharacter2D>().Platformed = false;
            _platformedObject = null;
        }
    }
}
