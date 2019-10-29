using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Game.Instance.Paused.AddListener(PausedChanged);
    }

    private void PausedChanged(bool paused)
    {
        Debug.Log(gameObject);
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(!paused);
        }
    }

    private void OnDestroy()
    {
        Game.Instance.Paused.RemoveListener(PausedChanged);
    }
}
