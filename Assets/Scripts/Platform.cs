using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Transform _platformedObject;
    private Vector3 _delta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {

            _platformedObject = other.transform;
            _platformedObject.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {
            _platformedObject.SetParent(null);
            _platformedObject = null;
        }
    }
}
