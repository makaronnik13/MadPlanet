using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerIdentity : MonoBehaviour
{
    public float ForceModificator = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        GetComponent<Animator>().SetTrigger("Damaged");
        Debug.Log("Damage");
    }


    public void Damage(Vector3 position, float dangerRadius)
    {
        Vector3 dif = position - transform.position;
        dif = new Vector3(dif.x, 0, dif.z).normalized*dangerRadius;
        GetComponent<Rigidbody>().AddForce(dif*-ForceModificator, ForceMode.Acceleration);
        Damage();
    }

    public void Die()
    {
       GetComponent<PlatformerCharacter2D>().Grab(true, 2f);
    }
}
