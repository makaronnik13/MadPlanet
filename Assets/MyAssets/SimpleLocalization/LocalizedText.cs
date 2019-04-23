using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Assets.SimpleLocalization
{
    /// <summary>
    /// Localize text component.
    /// </summary>
    

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        public bool LineBreak = false;
        public Localization.LocalizedString LocalizedString;

        private TextMeshProUGUI _text;
        private TextMeshProUGUI text
        {
            get
            {
                if (_text==null)
                {
                    _text = GetComponent<TextMeshProUGUI>();
                }
                return _text;
            }
        }

        public void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            string v = LocalizationManager.Localize(LocalizedString.Key, LocalizedString.Dictionary);
            if (LineBreak)
            {
                v = UseBreak(v);
            }
            text.text = v;
        }

        private string UseBreak(string v)
        {
            string s = "";
            int counter = 0;
            foreach (char ch in v)
            {
                if (ch == ' ' && counter>15)
                {
                    s += "\n";
                    counter = 0;
                }
                else
                {
                    s += ch;
                    counter++;
                }           
            }

            return s;
        }

        public void SetText(string s)
        {
            LocalizedString.Key = s;
            Localize();
        }
    }
}