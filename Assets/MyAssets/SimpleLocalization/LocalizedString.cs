using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    [System.Serializable]
    public class LocalizedString
    {
        public LocalizationDictionary Dictionary;
        public string Key = "";

        public string Text
        {
            get
            {
                if (!Dictionary)
                {
                    return Key;
                }
                return Dictionary.Translate(Key, LocalizationManager.Language);
            }
        }
    }
}
