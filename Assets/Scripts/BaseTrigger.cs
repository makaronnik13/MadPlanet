using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AnimtionsSwapper>())
        {
            other.GetComponent<AnimtionsSwapper>().SetSkin(AnimtionsSwapper.CharacterSkin.NoArmor);
        }

        if (other.GetComponent<Platformer2DUserControl>())
        {
            other.GetComponent<Platformer2DUserControl>().JumpingAvaliable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AnimtionsSwapper>())
        {
            other.GetComponent<AnimtionsSwapper>().SetSkin(AnimtionsSwapper.CharacterSkin.Armored);
        }

        if (other.GetComponent<Platformer2DUserControl>())
        {
            other.GetComponent<Platformer2DUserControl>().JumpingAvaliable = true;
        }
        
    }
}
