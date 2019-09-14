using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets._2D;

public class AtackModule : MonoBehaviour
{
    [SerializeField]
    private InteractableObject InteractionModule;

    public bool CanAtack = true;

    [SerializeField]
    private float AttackDelay;

    [SerializeField]
    private ParticleSystem Particles;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private bool canceled = false;

    private PlayerIdentity aim
    {
        get
        {
            Transform following = GetComponentInParent<MobLogic>().followingObject;
            if (following)
            {
                return following.GetComponent<PlayerIdentity>();
            }
            return null;
        }
    }

    private bool grabbing = false;
    public bool Grabbing
    {
        get
        {
            return grabbing;
        }
        set
        {
            grabbing = value;
            if (aim)
            {
                aim.GetComponent<PlatformerCharacter2D>().Grab(value);
            }
            InteractionModule.Active = grabbing;
        }
    }

    private Vector3 delta;


    private void OnTriggerExit(Collider other)
    {
        //Grabbing = false;
        //Animator.ResetTrigger("PreAttack");
    }

    public void Atack()
    {
        
        if (aim != null && CanAtack)
        {
            Animator.SetTrigger("Attack");
            Particles.Play();
            Grabbing = true;
            delta = transform.position;
        }
        //Animator.SetTrigger("Attack");
        //Particles.Play();
    }


    private void Update()
    {
        if (Grabbing && aim)
        {
            aim.transform.position = Vector3.Lerp(aim.transform.position, transform.position, Time.deltaTime*5);
        }
    }

    public void Release()
    {
        Grabbing = false;
        CanAtack = false;
        StartCoroutine(ContinueAttack());
    }

    private IEnumerator ContinueAttack()
    {
        yield return new WaitForSeconds(1);
        CanAtack = true;
    }
}