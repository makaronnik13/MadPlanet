using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEmmiter : MonoBehaviour
{
    public ParticleSystem Syst;

    public void Emmit()
    {
        Syst.Emit(30);
    }
}
