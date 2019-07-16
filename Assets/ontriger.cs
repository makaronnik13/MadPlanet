using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ontriger : MonoBehaviour
{
    public UnityEvent OnActivate;
    public GameObject GM_my_active;
    public GameObject GM_my_deactive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void ActivateGM()
    {
        GM_my_active.SetActive(true);

    }

    public void DeactivateGM()
    {
        GM_my_deactive.SetActive(false);

    }

    void OnTriggerEnter(Collider other)

    {
        OnActivate.Invoke();

        gameObject.SetActive(false);
    }

}
