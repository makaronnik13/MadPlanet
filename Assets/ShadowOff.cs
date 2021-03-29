using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowOff : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    // Start is called before the first frame update
    void Start()
    {
        spriteRend.receiveShadows = false;
      // spriteRend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
    }
}
