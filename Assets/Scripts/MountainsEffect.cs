using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MountainsEffect : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera camera;
    public float Delay = 0;
    public float ShakeTime = 6;
    public UnityEvent OnComplete;

    [ContextMenu("Activate")]
    public void Activate()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        yield return new WaitForSeconds(Delay);

        float t = 0;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        while (t<ShakeTime/4f)
        {
            t += Time.deltaTime;
            camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = t*4f;
            Game.Instance.Noise.SetState(2*t / ShakeTime);
            yield return null;
        }
        while (t > 0)
        {
            t -= Time.deltaTime;
            camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = t * 2f;
            Game.Instance.Noise.SetState(2*t /ShakeTime);
            yield return null;
        }
        Game.Instance.Noise.SetState(0);
        OnComplete.Invoke();
    }
}
