using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.sortingOrder = Mathf.Clamp(Mathf.RoundToInt((camera.position- transform.position).x*5), -1000, 1000);
    }
}
