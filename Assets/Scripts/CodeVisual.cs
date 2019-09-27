using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeVisual : MonoBehaviour
{
    public GameObject CodeBtnPrefab;
    public InteractionModule Module;

    // Start is called before the first frame update
    void Start()
    {
        Module.OnObjectChanged += ObjectChanged;
        Module.OnCodeProgressChanged += CodeProgressChanged;
        Module.OnCodesChanged += OnCodesChanged;
    }

    private void OnDestroy()
    {
        Module.OnObjectChanged -= ObjectChanged;
        Module.OnCodeProgressChanged += CodeProgressChanged;
    }

    private void Update()
    {
        if (Module.interactableObject)
        {
            transform.position = Module.interactableObject.transform.position + Module.interactableObject.EButtonOffset;
        }
    }

    private void OnCodesChanged()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        foreach (int i in Module.Codes)
        {
            GameObject newBtn = Instantiate(CodeBtnPrefab);
            newBtn.transform.SetParent(transform);
            newBtn.transform.SetAsFirstSibling();
            newBtn.transform.localRotation = Quaternion.identity;
            newBtn.transform.localPosition = Vector3.zero;
            newBtn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Module.GetCodeBtn(i).ToString();
        }
    }

    private void ObjectChanged(InteractableObject obj)
    {
        if (obj == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(obj.IneractionMode == InteractableObject.InteractionMode.Keys);

        OnCodesChanged();
        CodeProgressChanged();
            /*
            if (obj)
            {
                Animator.SetBool("Showing", true);
                transform.position = obj.transform.position + obj.EButtonOffset;
            }
            else
            {
                Animator.SetBool("Showing", false);
            }*/
        }

    private void CodeProgressChanged()
    {
        int i = Module.Codes.Count;
        foreach (Transform t in transform)
        {
            if (i > Module.CodeProgress)
            {
                Color c = t.GetComponentInChildren<Image>().color;
                c = new Color(c.r, c.g, c.b, 1f);
                t.GetComponentInChildren<Image>().color = c;
                c = t.GetComponentInChildren<TMPro.TextMeshProUGUI>().color;
                c = new Color(c.r, c.g, c.b, 1f);
                t.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = c;
            }
            else
            {
                Color c = t.GetComponentInChildren<Image>().color;
                c = new Color(c.r, c.g, c.b, 0.5f);
                t.GetComponentInChildren<Image>().color = c;
                c = t.GetComponentInChildren<TMPro.TextMeshProUGUI>().color;
                c = new Color(c.r, c.g, c.b, 0.5f);
                t.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = c;
            }

            t.localScale = Vector3.one;

            i--;
        }

        int btnId = Module.Codes.Count - Module.CodeProgress - 1;

        if (btnId>=0 && btnId< transform.childCount)
        {
                transform.GetChild(btnId).localScale = Vector3.one * 1.25f;
        }

    }
}
