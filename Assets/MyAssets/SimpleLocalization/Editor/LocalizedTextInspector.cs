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

    private void OnEnable()
    {
        _txt = (LocalizedText)target;
    }

    public override void OnInspectorGUI()
    {
        LocalizationStruct stru = _txt.LocalizedString.Dictionary.localisedStrings.FirstOrDefault(s=>s.key == _txt.LocalizedString.Key);
        List<string> strings = new List<string>();
        foreach (LocalizationStruct st in _txt.LocalizedString.Dictionary.localisedStrings)
        {
            strings.Add(st.key);
        }

        int id = 0;
        if (stru!=null)
        {
            id = _txt.LocalizedString.Dictionary.localisedStrings.IndexOf(stru);
        }
        _txt.LocalizedString.Key = _txt.LocalizedString.Dictionary.localisedStrings[EditorGUILayout.Popup(id, strings.ToArray())].key;

        _txt.LineBreak = EditorGUILayout.Toggle("Break lines", _txt.LineBreak);

        EditorUtility.SetDirty(target);
    }
}
