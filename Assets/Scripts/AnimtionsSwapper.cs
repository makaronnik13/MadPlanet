using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class AnimtionsSwapper : MonoBehaviour
{
    public enum CharacterSkin
    {
        NoArmor = 0,
        Armored = 1,
        Slug = 2,
        SlugNoEyes = 3,
        ArmorBag = 4,
        SlugSide = 5
    }

    [SerializeField]
    private RuntimeAnimatorController[] Animations;


    [SerializeField]
    private Animator Animator;

    private void Start()
    {
        SetPerson(Game.Instance.gameData.Skin);    
    }

    public void SetPerson(int i)
    {
        Animator.runtimeAnimatorController = Animations[i];
        Game.Instance.gameData.Skin = i;
    }

    public void SetSkin(CharacterSkin skin)
    {
        SetPerson((int)skin);
    }
}
