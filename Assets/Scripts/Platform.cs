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
            _delta = transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {
            _platformedObject = null;
        }
    }

    private void Update()
    {
        if (_platformedObject)
        {
            _platformedObject.transform.position += transform.position-_delta;
            _delta = transform.position;
        }
    }
}
