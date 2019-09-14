using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionModule : MonoBehaviour
{
    public Action<InteractableObject> OnObjectChanged = (o)=> { };
    public Action<float, float> OnFillChanged = (val, max) => { };

    private float _interactionDecreaseMultiplyer = 1f;

    private float _interactionFillMax;
    private float interactionFillMax
    {
        set
        {
            _interactionFillMax = value;
            OnFillChanged(_interactionFill, _interactionFillMax);
        }
    }

    private float _interactionFill;
    private float interactionFill
    {
        set
        {
            _interactionFill = value;
            OnFillChanged(_interactionFill, _interactionFillMax);
        }
    }

    private InteractableObject _interactableObject;
    public InteractableObject interactableObject
    {
        get
        {
            return _interactableObject;
        }
        set
        {
            Debug.Log(_interactableObject);

            _interactableObject = value;
            if (_interactableObject)
            {
                _interactionDecreaseMultiplyer = _interactableObject.DecreaseMultiplyer;
                interactionFillMax = _interactableObject.Delay;
            }
            else
            {
                interactionFill = 0;
                interactionFillMax = 0;
            }
            OnObjectChanged(_interactableObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>() && other.GetComponent<InteractableObject>().Active)
        {
            interactableObject = other.GetComponent<InteractableObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableObject obj = other.GetComponent<InteractableObject>();
        if (obj == interactableObject)
        {
            interactableObject = null;
        }
    }

    private void Update()
    {
        if (interactableObject)
        {
                if (Input.GetKey(KeyCode.E))
                {
                    if (interactableObject.Delay == 0)
                    {
                        interactableObject.Activate();
                        interactableObject = null;
                    }
                    else
                    {
                        interactionFill = Mathf.Clamp(_interactionFill + Time.deltaTime, 0, 1000);
                        if (_interactionFill >= _interactionFillMax)
                        {
                            interactableObject.Activate();
                            interactableObject = null;
                        }
                    }           
                }
                else
                {
                    interactionFill = Mathf.Clamp(_interactionFill - Time.deltaTime*_interactionDecreaseMultiplyer, 0, 1000);
                }
        }
        else
        {
            interactionFill = Mathf.Clamp(_interactionFill - Time.deltaTime*_interactionDecreaseMultiplyer, 0, 1000);
        }
    }
}
