﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize dropdown component.
	/// </summary>
    [RequireComponent(typeof(Dropdown))]
    public class LocalizedDropdown : MonoBehaviour
    {
        public string[] LocalizationKeys;
        public LocalizationDictionary Dictionary;

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
	        var dropdown = GetComponent<Dropdown>();

			for (var i = 0; i < LocalizationKeys.Length; i++)
	        {
		        dropdown.options[i].text = LocalizationManager.Localize(LocalizationKeys[i], Dictionary);
	        }

	        dropdown.captionText.text = LocalizationManager.Localize(LocalizationKeys[dropdown.value], Dictionary);
        }
    }
}