using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxed : MonoBehaviour
{
    
    private Vector3 pz;
    private Vector3 _startPos;
    private Vector3 _aimPos;

    public float MoveModifier;
    public float RandomMoveModifier;
    public float RandomMoveSpeed = 1f;

    private void Start()
    {
        _startPos = transform.localPosition;    
    }

    /*
    void OnApplicationFocus(bool boolFocus)
    {
        if (boolFocus)
        {
            transform.localPosition = _startPos;
        }
        else
        {

        }
    }*/

    // Update is called once per frame
    void Update ()
    {
        Vector2 mousePos = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height));
            Vector3 pz = Camera.main.ScreenToViewportPoint(mousePos);

            pz += -Vector3.one / 2f;
            pz *= 500f;
            pz.z = 0;

            _aimPos = new Vector3(_startPos.x - (pz.x * MoveModifier), _startPos.y - (pz.y * MoveModifier), 0);
            Vector3 offset = Vector3.zero;
            //new Vector3(UnityEngine.Random.Range(-RandomMoveModifier, RandomMoveModifier), UnityEngine.Random.Range(-RandomMoveModifier, RandomMoveModifier), UnityEngine.Random.Range(-RandomMoveModifier, RandomMoveModifier));

            transform.localPosition = Vector3.Lerp(transform.localPosition, _aimPos + offset, Time.deltaTime);    
    }
}
