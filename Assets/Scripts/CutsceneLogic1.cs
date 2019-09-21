using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneLogic1 : MonoBehaviour
{
    [SerializeField]
    private GameObject Eyes;

    public void ShowScene()
    {
        Eyes.SetActive(true);
        Eyes.transform.position = FindObjectOfType<PlayerIdentity>().transform.position;
        StartCoroutine(FreeEyes());
    }

    private IEnumerator FreeEyes()
    {
        yield return new WaitForSeconds(1.2f);
        Eyes.GetComponentInChildren<MobLogic>().gameObject.transform.SetParent(null);
        Destroy(Eyes);
    }
}
