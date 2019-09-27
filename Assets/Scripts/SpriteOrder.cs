using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour
{
    private SpriteRenderer sprite;
    private ParticleSystem particles;
    private Transform camera;
    public SpriteOrder ParentOrder;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        sprite = GetComponent<SpriteRenderer>();
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        int order = Mathf.Clamp(Mathf.RoundToInt((camera.position - transform.position).x * 5), -1000, 1000);

        if (ParentOrder)
        {
            //float parent = Mathf.Clamp(Mathf.RoundToInt((camera.position - ParentOrder.transform.position).x * 5), -1000, 1000);
            //float child = Mathf.Clamp(Mathf.RoundToInt((camera.position - transform.position).x * 5), -1000, 1000);

                order = ParentOrder.sprite.sortingOrder - 2;
        }


        if (particles)
        {
            particles.GetComponent<Renderer>().sortingOrder = order;
        }
        if (sprite)
        {
            sprite.sortingOrder = order;
        }

        
    }
}
