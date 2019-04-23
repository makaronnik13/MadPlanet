using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Asset usage example.
	/// </summary>
	public class Example : MonoBehaviour
	{
		public TextMeshProUGUI FormattedText;

		/// <summary>
		/// Called on app start.
		/// </summary>
		public void Awake()
		{
			

			switch (Application.systemLanguage)
			{
				case SystemLanguage.German:
					LocalizationManager.Language = SystemLanguage.German;
					break;
				case SystemLanguage.Russian:
					LocalizationManager.Language = SystemLanguage.Russian;
					break;
				default:
					LocalizationManager.Language = SystemLanguage.English;
					break;
			}

			// This way you can insert values to localized strings.
			FormattedText.text = LocalizationManager.Localize("Settings.PlayTime", null, TimeSpan.FromHours(10.5f).TotalHours);

			// This way you can subscribe to localization changed event.
			LocalizationManager.LocalizationChanged += () => FormattedText.text = LocalizationManager.Localize("Settings.PlayTime", null, TimeSpan.FromHours(10.5f).TotalHours);
		}

		/// <summary>
		/// Change localization at runtime
		/// </summary>
		public void SetLocalization(SystemLanguage localization)
		{
			LocalizationManager.Language = localization;
		}

		/// <summary>
		/// Write a review.
		/// </summary>
		public void Review()
		{
			Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/120113");
		}
	}
}