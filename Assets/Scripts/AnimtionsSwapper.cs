using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class AnimtionsSwapper : MonoBehaviour
{
    public enum CharacterSkin
    {
        NoArmor,
        Armored,
        Slug,
        SlugNoEyes,
        ArmorBag,
        SlugSide
    }

    [SerializeField]
    private RuntimeAnimatorController[] Animations;


    [SerializeField]
    private Animator Animator;


    public void SetPerson(int i)
    {
        Animator.runtimeAnimatorController = Animations[i];
    }

    public void SetSkin(CharacterSkin skin)
    {
        SetPerson((int)skin);
    }
}
