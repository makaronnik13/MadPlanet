using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Localization;
using UnityEngine;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localization manager.
	/// </summary>
    public static class LocalizationManager
    {
		/// <summary>
		/// Fired when localization changed.
		/// </summary>
        public static event Action LocalizationChanged = () => { }; 

        private static readonly Dictionary<string, Dictionary<string, string>> Dictionary = new Dictionary<string, Dictionary<string, string>>();
        private static SystemLanguage _language = SystemLanguage.Unknown;

		/// <summary>
		/// Get or set language.
		/// </summary>
        public static SystemLanguage Language
        {
            get
            {
                if (_language == SystemLanguage.Unknown)
                {
                    if (PlayerPrefs.HasKey("Language"))
                    {
                        _language = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), PlayerPrefs.GetString("Language"));                
                    }
                    else
                    {
                        _language = Application.systemLanguage;
                        if (_language!= SystemLanguage.Russian && _language!=SystemLanguage.English)
                        {
                            _language = SystemLanguage.English;
                        }
                        PlayerPrefs.SetString("Language", _language.ToString());
                    }

                    Language = _language;
                }

                if (_language != SystemLanguage.Russian && _language != SystemLanguage.English)
                {
                    _language = SystemLanguage.English;
                }
                return _language;
            }
            set
            {              
                _language = value;
                LocalizationChanged();
                PlayerPrefs.SetString("Language", _language.ToString());
            }
        }



        /// <summary>
        /// Get localized value by localization key.
        /// </summary>
        public static string Localize(string localizationKey, LocalizationDictionary dict)
        {
            return dict.Translate(localizationKey, Language);
        }

      
		public static string Localize(string localizationKey, LocalizationDictionary dict, params object[] args)
        {
            var pattern = Localize(localizationKey, dict);

            return string.Format(pattern, args);
        }
    }
}