using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LocalizationManagerWindow : EditorWindow
{
    private int tab;
    private string newKeyName;
    private Vector2 scrollPosition;

    private static LocalizationDictionary _dictionary;
    public static LocalizationDictionary Dictionary
    {
        set
        {
            _dictionary = value;
            EditorWindow.GetWindow(typeof(LocalizationManagerWindow));
        }
    }

    public Rect windowRect = new Rect(100, 100, 200, 200);

 

    void OnGUI()
    {
        try
        {
            List<string> tabs = new List<string>();
            foreach (SystemLanguage lang in _dictionary.Languages)
            {
                tabs.Add(lang.ToString().Substring(0, 3));
            }

            tabs.Add("+");

            int tabNew = GUILayout.Toolbar(tab, tabs.ToArray(), GUILayout.Width(tabs.Count * 50));

            if (tab!=tabNew)
            {
                GUI.FocusControl(null);
                tab = tabNew;
            }

            if (tab == tabs.Count - 1)
            {
                _dictionary.Languages.Add(SystemLanguage.Unknown);
            }
            else
            {

                EditorGUILayout.BeginHorizontal();
                if (_dictionary.Languages[tab] == SystemLanguage.Unknown)
                {
                    SystemLanguage newSl = (SystemLanguage)EditorGUILayout.EnumPopup("Language: ", _dictionary.Languages[tab], GUILayout.Width(300));
                    if (!_dictionary.Languages.Contains(newSl))
                    {
                        _dictionary.Languages[tab] = newSl;
                    }
                }

                GUI.color = Color.red;
                if (GUILayout.Button("X", GUILayout.Width(50)))
                {
                    _dictionary.Languages.RemoveAt(tab);
                }
                GUI.color = Color.white;
                EditorGUILayout.EndHorizontal();

                if (_dictionary.Languages[tab] != SystemLanguage.Unknown)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Keys", GUILayout.Width(200));
                    EditorGUILayout.LabelField("Translations");
                    EditorGUILayout.EndHorizontal();

                    DrawKeys();

                    
                    EditorGUILayout.BeginHorizontal();
                    newKeyName = EditorGUILayout.TextField(newKeyName, GUILayout.Width(200));
                    if (GUILayout.Button("+", GUILayout.Width(150)))
                    {
                        _dictionary.AddString(newKeyName);
                    }
                    EditorGUILayout.EndHorizontal();

                }

            }  
        }
        catch
        {

        }
    }

    private void DrawKeys()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUIStyle.none, GUI.skin.box, GUILayout.Height(position.height - EditorGUIUtility.singleLineHeight*7));
        LocalizationStruct removingStruct = null;
        foreach (LocalizationStruct ls in _dictionary.localisedStrings)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(ls.key, GUILayout.Width(200));
            ls.Translation(_dictionary.Languages[tab]).translation = EditorGUILayout.TextField(ls.Translation(_dictionary.Languages[tab]).translation);

            GUI.color = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                removingStruct = ls;
            }
            GUI.color = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        if (removingStruct != null)
        {
            _dictionary.localisedStrings.Remove(removingStruct);
        }
        EditorGUILayout.EndScrollView();
    }

    private void OnDisable()
    {
        EditorUtility.SetDirty(_dictionary);
        AssetDatabase.SaveAssets();
    }
}
