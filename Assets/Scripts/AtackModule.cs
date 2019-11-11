using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets._2D;

public class AtackModule : MonoBehaviour
{
    public AudioSource SoundSource;

    public AudioClip AtackSound, ReleaseSound;

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
    private float ReleaseTime = 20;

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

    private Transform grabbingTransform = null;
    public Transform GrabbingTransform
    {
        get
        {
            return grabbingTransform;
        }
        set
        {
            if (value)
            {

                value.SetParent(transform);
                value.GetComponentInChildren<SpriteOrder>().ParentOrder = transform.parent.GetComponentInChildren<SpriteOrder>();
            }
            else
            {
                if (grabbingTransform)
                {
                    grabbingTransform.SetParent(null);
                    grabbingTransform.GetComponentInChildren<SpriteOrder>().ParentOrder = null;
                }
            }

            grabbingTransform = value;
            if (aim)
            {
                aim.GetComponent<PlatformerCharacter2D>().Grab(grabbingTransform!=null);
            }
            InteractionModule.Active = grabbingTransform!=null; 
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
        if (aim != null && CanAtack && !aim.GetComponent<PlatformerCharacter2D>().Grabed && !aim.GetComponent<PlatformerCharacter2D>().Drawn && !aim.GetComponent<PlatformerCharacter2D>().Platformed)
        {
            SoundSource.PlayOneShot(AtackSound);
            Animator.SetBool("Grab", true);
            //Particles.Play();
            GrabbingTransform = aim.transform;
            delta = transform.position;
            FindObjectOfType<InteractionModule>().interactableObject = GetComponent<InteractableObject>();
            GrabbingTransform.SetParent(transform);
            StartCoroutine(MoveToZero(0.5f));
        }
        //Animator.SetTrigger("Attack");
        //Particles.Play();
    }

    private IEnumerator MoveToZero(float v)
    {
        float t = 0;
        Vector3 startPos = GrabbingTransform.localPosition;
        while (t<=v)
        {
            if (GrabbingTransform == null)
            {
                break;
            }
            GrabbingTransform.localPosition = Vector3.Lerp(startPos, Vector3.zero, t/v);
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void Release()
    {
        SoundSource.PlayOneShot(ReleaseSound);
        GrabbingTransform.SetParent(null);
        CanAtack = false;
        Animator.SetBool("Grab", false);
        FindObjectOfType<InteractionModule>().interactableObject = null;
        StartCoroutine(ContinueAttack());
        GrabbingTransform = null;
    }

    private IEnumerator ContinueAttack()
    {
        yield return new WaitForSeconds(ReleaseTime);
        CanAtack = true;
    }
}