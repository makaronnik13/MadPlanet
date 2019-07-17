using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimtionsSwapper : MonoBehaviour
{
    public RuntimeAnimatorController NoArmor, Armor;

    [SerializeField]
    private Animator Animator;

    public void SetArmor(bool v)
    {
        if (v)
        {
            Animator.runtimeAnimatorController = Armor;
        }
        else
        {
            Animator.runtimeAnimatorController = NoArmor;
        }
    }
}
