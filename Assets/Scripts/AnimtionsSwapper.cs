using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimtionsSwapper : MonoBehaviour
{
    public RuntimeAnimatorController NoArmor, Armor, Slug, SlugWithoutEyes;

    [SerializeField]
    private Animator Animator;

    [ContextMenu("Person")]
    private void SetPerson()
    {

        if (Animator.runtimeAnimatorController == Slug)
        {
            SetEyes(false);
            return;
        }
        if (Animator.runtimeAnimatorController == SlugWithoutEyes)
        {
            SetArmor(true);
            return;
        }
        if (Animator.runtimeAnimatorController == Armor)
        {
            SetArmor(false);
            return;
        }
        if (Animator.runtimeAnimatorController == NoArmor)
        {
            SetSlug(true);
            return;
        }

        Animator.runtimeAnimatorController = Armor;
    }

    public void SetSlug(bool v)
    {
        Debug.Log("set slug");
        if (v)
        {
            Animator.runtimeAnimatorController = Slug;
        }
        else
        {
            Animator.runtimeAnimatorController = Armor;
        }
    }

    public void SetArmor(bool v)
    {
        Debug.Log("set armor");
        if (v)
        {
            Animator.runtimeAnimatorController = Armor;
        }
        else
        {
            Animator.runtimeAnimatorController = NoArmor;
        }
    }


    public void SetEyes(bool v)
    {
        Debug.Log("set eyes");
        if (v)
        {
            Animator.runtimeAnimatorController = Slug;
        }
        else
        {
            Animator.runtimeAnimatorController = SlugWithoutEyes;
        }
    }
}
