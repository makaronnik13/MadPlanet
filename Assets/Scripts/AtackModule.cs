using System;
using System.Collections;
using UnityEngine;


public class AtackModule: MonoBehaviour
{
    private enum AttackType
    {
        Push,
        Death
    }

    [SerializeField]
    private AttackType AType = AttackType.Push;

    [SerializeField]
    private float AttackDelay;

    [SerializeField]
    private ParticleSystem Particles;

    [SerializeField]
    private Animator Animator;

    public void Atack()
    {
        Animator.SetTrigger("PreAttack");
        StartCoroutine(PerformAttack());
        Debug.Log("ATTACK");
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(AttackDelay);
        switch (AType)
        {
            case AttackType.Death:
                FindObjectOfType<PlayerIdentity>().Damage();
                break;
            case AttackType.Push:
                FindObjectOfType<PlayerIdentity>().GetComponent<Rigidbody>().AddForce(FindObjectOfType<PlayerIdentity>().transform.position-transform.position * 150, ForceMode.Force);
                break;
        }
        Animator.SetTrigger("Attack");
        Particles.Play();
    }
}