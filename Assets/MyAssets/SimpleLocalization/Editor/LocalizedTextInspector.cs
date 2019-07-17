using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.SimpleLocalization;
using System.Linq;

[CustomEditor(typeof(LocalizedText))]
public class LocalizedTextInspector : Editor
{
    private LocalizedText _txt;
    private int index = 0;

    private void OnEnable()
    {
        _txt = (LocalizedText)target;

        List<string> strings = new List<string>();

        if (_txt.LocalizedString.Dictionary != null)
        {
            // _txt.LocalizedString.Dictionary.localisedStrings.FirstOrDefault(s => s.key == _txt.LocalizedString.Key);
            foreach (LocalizationStruct st in _txt.LocalizedString.Dictionary.localisedStrings)
            {
                strings.Add(st.key);
            }

            index = strings.IndexOf(_txt.LocalizedString.Key);
        }
        else
        {
            index = 0;
        }

    }

    public override void OnInspectorGUI()
    {
        _txt.LocalizedString.Dictionary = (LocalizationDictionary)EditorGUILayout.ObjectField(_txt.LocalizedString.Dictionary, typeof(LocalizationDictionary), false);

        LocalizationStruct stru = null;
        List<string> strings = new List<string>();

        if (_txt.LocalizedString.Dictionary!=null)
        {
           // _txt.LocalizedString.Dictionary.localisedStrings.FirstOrDefault(s => s.key == _txt.LocalizedString.Key);
            foreach (LocalizationStruct st in _txt.LocalizedString.Dictionary.localisedStrings)
            {
                strings.Add(st.key);
            }

        }


        if (_txt.LocalizedString.Dictionary != null)
        {
            int v = EditorGUILayout.Popup(index, strings.ToArray());

            if (index!= v)
            {
                index = v;
                Debug.Log(index);
            }
            _txt.LocalizedString.Key = _txt.LocalizedString.Dictionary.localisedStrings[index].key;
        }

        _txt.LineBreak = EditorGUILayout.Toggle("Break lines", _txt.LineBreak);

        EditorUtility.SetDirty(target);
    }
}
