using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class LocalizationStruct
{
    public string key;
    public List<LocalizationPair> translations = new List<LocalizationPair>();

    public LocalizationPair Translation(SystemLanguage lng)
    {
        LocalizationPair pair = translations.FirstOrDefault(l => l.language == lng);
        if (pair == null)
        {
            pair = new LocalizationPair(lng, "");
            translations.Add(pair);
        }
        return pair;
    }
}
