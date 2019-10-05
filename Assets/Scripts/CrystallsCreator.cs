using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallsCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject CrystallPrefab;

    private GameObject _crystall;

    public void Release()
    {
        if (_crystall)
        {
            _crystall.AddComponent<Rigidbody>();
            _crystall.GetComponent<Rigidbody>().useGravity = true;
            Destroy(_crystall, 5f);
            _crystall = null;
        }
    }

    public void Grab()
    {
        _crystall = Instantiate(CrystallPrefab);
        _crystall.transform.SetParent(transform);
        _crystall.transform.localPosition = Vector3.zero;
        _crystall.transform.localRotation = Quaternion.identity;
        
    }
}
