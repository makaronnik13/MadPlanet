using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    public InteractionModule Module;
    public Animator Animator;
    public Image FillImg;

    // Start is called before the first frame update
    void Start()
    {
        Module.OnObjectChanged += InteractableObjectChanged;
        Module.OnFillChanged += FillChanged;
        Module.OnCodeProgressChanged += CodeProgressChanged;
    }

    private void CodeProgressChanged()
    {
        
    }

    private void FillChanged(float val, float max)
    {
        FillImg.gameObject.SetActive(max>0);
        FillImg.fillAmount = (val+0.0f) / max;
    }

    private void OnDestroy()
    {
        Module.OnObjectChanged -= InteractableObjectChanged;
        Module.OnFillChanged += FillChanged;
    }

    private void Update()
    {
        if (Module.interactableObject)
        {
            transform.position = Module.interactableObject.transform.position + Module.interactableObject.EButtonOffset;
        }
    }

    private void InteractableObjectChanged(InteractableObject obj)
    {
        if (obj == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(obj.IneractionMode == InteractableObject.InteractionMode.Normal);

        if (obj)
        {
            Animator.SetBool("Showing", true);
            transform.position = obj.transform.position + obj.EButtonOffset;

            string[] names = Input.GetJoystickNames();
            for (int x = 0; x < names.Length; x++)
            {
                if (names[x].Length == 19)
                {
                    Debug.Log("PS4 CONTROLLER IS CONNECTED");
                    GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "□";
                }
                if (names[x].Length == 33)
                {
                    Debug.Log("XBOX ONE CONTROLLER IS CONNECTED");
                    //set a controller bool to true
                    GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "X";
                }
            }
        }
        else
        {
            Animator.SetBool("Showing", false);
        }
    }
}
