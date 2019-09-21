using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class InteractionModule : MonoBehaviour
{
    public Action<InteractableObject> OnObjectChanged = (o)=> { };
    public Action<float, float> OnFillChanged = (val, max) => { };
    public Action OnCodeProgressChanged = () => { };
    public Action OnCodesChanged = () => { };

    public List<int> Codes = new List<int>();
    private int _codeProgress = 0;
    public int CodeProgress
    {
        get
        {
            return _codeProgress;
        }
        set
        {
            _codeProgress = value;
            if (_codeProgress == Codes.Count)
            {
                interactableObject.Activate();
                interactableObject = null;
            }
            OnCodeProgressChanged();
        }
    }

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

    public string GetCodeBtn(int code)
    {
        switch (code)
        {
            case 0:
                return "W";
            case 1:
                return "A";
            case 2:
                return "S";
            case 3:
                return "D";
        }

        return "undefined";
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
            if (value == _interactableObject)
            {
                return;
            }
            Debug.Log(value);
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
            if (interactableObject && interactableObject.IneractionMode == InteractableObject.InteractionMode.Keys)
            {
                ClearCodes();
            }
            OnObjectChanged(_interactableObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>() && other.GetComponent<InteractableObject>().Active && !GetComponent<PlatformerCharacter2D>().Grabed)
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

    private void ClearCodes()
    {
        Codes = new List<int>();
        for (int i = 0; i < interactableObject.Delay; i++)
        {
            Codes.Add(UnityEngine.Random.Range(0, 4));
        }
        CodeProgress = 0;
        OnCodesChanged();
    }

    private void Update()
    {
        if (interactableObject)
        {
            if (interactableObject.IneractionMode == InteractableObject.InteractionMode.Keys)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.A))
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        if (Codes[CodeProgress] == 0)
                        {
                            CodeProgress++;
                        }
                        else
                        {
                            ClearCodes();
                        }
                        return;
                    }

                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        if (Codes[CodeProgress] == 1)
                        {
                            CodeProgress++;
                        }
                        else
                        {
                            ClearCodes();
                        }
                        return;
                    }

                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        if (Codes[CodeProgress] == 2)
                        {
                            CodeProgress++;
                        }
                        else
                        {
                            ClearCodes();
                        }
                        return;
                    }

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        if (Codes[CodeProgress] == 3)
                        {
                            CodeProgress++;
                        }
                        else
                        {
                            ClearCodes();
                        }
                        return;
                    }
                }
                   
            }
            else
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
                    interactionFill = Mathf.Clamp(_interactionFill - Time.deltaTime * _interactionDecreaseMultiplyer, 0, 1000);
                }
            }
        }
        else
        {
            interactionFill = Mathf.Clamp(_interactionFill - Time.deltaTime*_interactionDecreaseMultiplyer, 0, 1000);
        }
    }
}
