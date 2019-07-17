using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUndestroyable : MonoBehaviour
{
    void Awake()
    {      
        DontDestroyOnLoad(this.gameObject);
    }
}
