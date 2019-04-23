using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizationDictionary))]
public class LocalizationDictionaryInspector : Editor
{
    private LocalizationDictionary _dictionary;
    private string _languagesDecription;
    private int _strings;
    private const string UrlPattern = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";

    private void OnEnable()
    {
        _dictionary = (LocalizationDictionary)target;

        _languagesDecription = "";
        int i = 0;
        foreach (SystemLanguage sl in _dictionary.Languages)
        {
            _languagesDecription += sl;
            if (sl != _dictionary.Languages[_dictionary.Languages.Count - 1])
            {
                _languagesDecription += ", ";
            }
            i++;
            if (i > 2)
            {
                _languagesDecription += "\n";
                i = 0;
            }
        }
        _strings = _dictionary.localisedStrings.Count;

    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Edit"))
        {
            LocalizationManagerWindow.Dictionary = _dictionary;
        }

        _dictionary.TableId = EditorGUILayout.TextField("Table Id", _dictionary.TableId);
        _dictionary.PageId = EditorGUILayout.TextField("Page Id", _dictionary.PageId);


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Import"))
        {
            Import();
        }
        if (GUILayout.Button("Export"))
        {
            Export();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Strings: " + _strings);
        EditorGUILayout.LabelField("Languages: ", _languagesDecription, GUILayout.Height(EditorGUIUtility.singleLineHeight * 3));
    }

    private void Import()
    {
        var url = string.Format(UrlPattern, _dictionary.TableId, _dictionary.PageId);

        Debug.Log(url);

        Downloader.Download(url, www =>
        {
            if (www.error == null)
            {
                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                string str = enc.GetString(www.bytes);
                Read(str);
                Debug.Log("Import <color=green>sucsess!</color>");
            }
            else
            {
                Debug.Log("Import <color=red>failed!</color>");
            }
            DestroyImmediate(Downloader.Instance.gameObject);
        });
    }

    private void Export()
    {
        foreach (LocalizationStruct s in _dictionary.localisedStrings)
        {
            Send(s);
        }

//        DestroyImmediate(Downloader.Instance.gameObject);
    }

    private void Read(string text)
    {
        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");


        Regex r = new Regex(@"(?m)^[^""\r\n]*(?:(?:""[^""]*"")+[^""\r\n]*)*");
        MatchCollection m = r.Matches(text);
       
        List<string[]> values = new List<string[]>();


        string[] lines =  m.Cast<Match>().Select(ma => ma.Value).ToArray();
        foreach (string line in lines)
        {
            String[] Fields = CSVParser.Split(line);
            int i = 0;
            foreach (string s in Fields)
            {
                if (s.StartsWith("\"")&& s.EndsWith("\""))
                {
                    Fields[i] = s.Substring(1, s.Length-2);
                    Fields[i] = RemoveDoubleQuotes(Fields[i]);
                }

                i++;
            }
            values.Add(Fields);
        }

        var languages = values[0].ToList();
        languages.RemoveAt(0);

        _dictionary.Languages = new List<SystemLanguage>();
        foreach (string lang in languages)
        {
            Debug.Log(lang);
            SystemLanguage newLang = SystemLanguage.Unknown;
            try
            {
                newLang = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), lang);
            }
            catch
            {
                newLang = SystemLanguage.Unknown;
            }
            Debug.Log(newLang);

            _dictionary.Languages.Add(newLang);
        }

        _dictionary.localisedStrings = new List<LocalizationStruct>();

        for (var i = 1; i < values.Count; i++)
        {
            LocalizationStruct locStruct = new LocalizationStruct();
            locStruct.key = values[i][0];
            for (var j = 1; j <= languages.Count; j++)
            {
                locStruct.translations.Add(new LocalizationPair(_dictionary.Languages[j - 1], values[i][j]));
            }

            _dictionary.localisedStrings.Add(locStruct);
        }

        EditorUtility.SetDirty(_dictionary);
        AssetDatabase.SaveAssets();
    }

    private string RemoveDoubleQuotes(string text)
    {
        Regex r = new Regex("(\")\\1+");
        MatchCollection m = r.Matches(text);
        return Regex.Replace(text, "(\")\\1+", "\"");
    }

    /*
    private void Read(string text)
    {
        text = ReplaceMarkers(text);
        var matches = Regex.Matches(text, "\".+?\"");
        foreach (Match match in matches)
        {
            text = text.Replace(match.Value, match.Value.Replace("\"", null).Replace(",", "[comma]"));
        }

        var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        var languages = lines[0].Trim().Split(',').ToList();

        languages = languages.GetRange(1, languages.Count - 1);

        _dictionary.Languages = new List<SystemLanguage>();
        foreach (string lang in languages)
        {
            SystemLanguage newLang = SystemLanguage.Unknown;
            Enum.TryParse(lang, out newLang);
            _dictionary.Languages.Add(newLang);
        }

        _dictionary.localisedStrings = new List<LocalizationStruct>();

        for (var i = 1; i < lines.Length; i++)
        {
            Debug.Log(lines[i]);
            var columns = lines[i].Split(',').Select(j => j.Replace("[comma]", ",")).ToList();
            LocalizationStruct locStruct = new LocalizationStruct();
            locStruct.key = columns[0];

            
            for (var j = 1; j <= languages.Count; j++)
            {
 
                locStruct.translations.Add(new LocalizationPair(_dictionary.Languages[j - 1], columns[j]));
            }

            _dictionary.localisedStrings.Add(locStruct);
        }

        EditorUtility.SetDirty(_dictionary);
        AssetDatabase.SaveAssets();
    }
    */
    private static string ReplaceMarkers(string text)
    {
        return text.Replace("[Newline]", "\n");
    }


    public void Send(LocalizationStruct str)
    {
        Downloader.Send(str, "https://docs.google.com/forms/d/e/1FAIpQLSduKqkQkdWroZcOZUBm_mgWIVkMccMjZt6J_QqSN0KBoZokKw/formResponse");
    }

}
