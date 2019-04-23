using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationPair
{
    public SystemLanguage language;
    public string translation;

    public LocalizationPair(SystemLanguage lng, string v)
    {
        this.language = lng;
        this.translation = v;
    }
}
