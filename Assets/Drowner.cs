using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Drowner : MonoBehaviour
{
    [SerializeField]
    private float DrownTime = 3;

    public void Drow(bool v = true)
    {
        FindObjectOfType<PlatformerCharacter2D>().SetDrawn(v, DrownTime);
    }
}
