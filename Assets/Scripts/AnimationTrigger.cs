using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTrigger : MonoBehaviour
{
    public List<UnityEvent> Events;

    public void Trigger(int i)
    {
        Events[i].Invoke();
    }
}
