using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointingArrow : MonoBehaviour
{
    [SerializeField]
    private Animator Arrow;

    [SerializeField]
    private MobLogic Mob;

    [SerializeField]
    private float MaxDistance;

    private void Update()
    {
        if (Mob.followingObject)
        {
            Arrow.SetTrigger("Active");
            float dist = Vector3.Distance(Mob.transform.position, Mob.followingObject.transform.position);

            Arrow.SetBool("FarAway", dist>MaxDistance*0.75f);
            if (dist>MaxDistance)
            {
                FindObjectOfType<PlayerIdentity>().Die();
            }
        }
    }
}
