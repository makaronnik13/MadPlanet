using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Localization;
using UnityEngine;

[CreateAssetMenu(fileName = "Dictionary", menuName = "Localization/Dictionary")]
public class LocalizationDictionary: ScriptableObject
{
    public List<string> Keys
    {
        get
        {
            return localisedStrings.Select(s=>s.key).ToList();
        }
    }

    public List<LocalizationStruct> localisedStrings = new List<LocalizationStruct>();

    public List<SystemLanguage> Languages = new List<SystemLanguage>();

    public string TableId, PageId;

    public void AddString(string sName)
    {
        LocalizationStruct s = new LocalizationStruct();
        s.key = sName;
        foreach (SystemLanguage sl in Languages)
        {
            s.translations.Add(new LocalizationPair(sl, ""));
        }
        localisedStrings.Add(s);
    }

    public string Translate(string localizationKey, SystemLanguage lang)
    {
        if (!Languages.Contains(lang))
        {
            Debug.LogWarning("No language "+lang+" in dictionary!");
            return localizationKey;
        }

        LocalizationStruct str = localisedStrings.FirstOrDefault(s=>s.key == localizationKey);
        if (str == null)
        {
            Debug.LogWarning("No key " + localizationKey + " in dictionary!");
            return localizationKey;
        }

        return str.translations.FirstOrDefault(t=>t.language == lang).translation;
    }

}