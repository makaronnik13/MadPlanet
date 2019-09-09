using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanAndRobot : MonoBehaviour
{
    [SerializeField]
    private Animator Dan, Robot;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayCourutine());
    }

    private IEnumerator PlayCourutine()
    {
        while (true)
        {
            RandomAction();
            yield return new WaitForSeconds(1f);
        }
    }

    private void RandomAction()
    {
        float f = UnityEngine.Random.value;

        if (f<0.3)
        {
            Dan.SetTrigger("Gaze");
        }
        else if (f<0.8)
        {
            Dan.SetTrigger("Foot");
        }
        else
        {
            Dan.SetTrigger("Card");
            StartCoroutine(RoboPlay());
        }
    }

    private IEnumerator RoboPlay()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f,2f));
        Robot.SetTrigger("Play");
    }
}
