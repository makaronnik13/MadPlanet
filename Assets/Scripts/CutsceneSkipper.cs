using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneSkipper : MonoBehaviour
{
    public UnityEvent OnSkip;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal"))>0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            OnSkip.Invoke();
        }
    }
}
