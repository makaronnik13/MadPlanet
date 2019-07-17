using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifractorController : MonoBehaviour
{
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        Game.Instance.Noise.AddListener(NoiseChanged);
    }

    private void NoiseChanged(float v)
    {
        mat.SetFloat("_DistoreEffect", v);
    }

}
