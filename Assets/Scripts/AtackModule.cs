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

   // [SerializeField]
   // private ParticleSystem Particles;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private bool canceled = false;

    [SerializeField]
    private float ReleaseTime = 2;

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
        if (aim != null && CanAtack && !aim.GetComponent<PlatformerCharacter2D>().Grabed)
        {
            Animator.SetBool("Grab", true);
            //Particles.Play();
            Grabbing = true;
            delta = transform.position;
            FindObjectOfType<InteractionModule>().interactableObject = GetComponent<InteractableObject>();
        }
        //Animator.SetTrigger("Attack");
        //Particles.Play();
    }


    private void Update()
    {
        if (Grabbing && aim)
        {
            Vector3 dir = transform.position - aim.transform.position;
            dir = dir.normalized * Mathf.Min(dir.magnitude, 0.2f);
            aim.transform.Translate(dir);
        }
    }

    public void Release()
    {
        Grabbing = false;
        CanAtack = false;
        Animator.SetBool("Grab", false);
        FindObjectOfType<InteractionModule>().interactableObject = null;
        StartCoroutine(ContinueAttack());
    }

    private IEnumerator ContinueAttack()
    {
        yield return new WaitForSeconds(ReleaseTime);
        CanAtack = true;
    }
}