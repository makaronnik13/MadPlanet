using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets._2D;

public class AtackModule: MonoBehaviour
{
    private enum AttackType
    {
        Push,
        Grab
    }

    [SerializeField]
    private AttackType AType = AttackType.Push;

    [SerializeField]
    private float AttackForce = 5000f;

    [SerializeField]
    private float AttackDelay;

    [SerializeField]
    private ParticleSystem Particles;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private bool canceled = false;

    [SerializeField]
    private PlayerIdentity aim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerIdentity>())
        {
            aim = other.GetComponent<PlayerIdentity>();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        aim = null;
        Animator.ResetTrigger("PreAttack");
    }

    public void Atack()
    {
        Animator.SetTrigger("PreAttack");
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(AttackDelay);

        if (aim != null)
        {
            switch (AType)
            {
                case AttackType.Grab:
                    GetComponentInChildren<GrabModule>().SetGrab(true);
                    break;
                case AttackType.Push:
                    /*
                    Vector3 diff = FindObjectOfType<PlayerIdentity>().transform.position - transform.position;
                    diff = new Vector3(diff.x, 0, diff.z);
                    diff = diff.normalized;
                    FindObjectOfType<PlayerIdentity>().GetComponent<Rigidbody>().AddForce(diff * AttackForce, ForceMode.Acceleration);
        */
                    aim.Die();
                    break;
            }
            Animator.SetTrigger("Attack");
            Particles.Play();
        }

        
    }

}