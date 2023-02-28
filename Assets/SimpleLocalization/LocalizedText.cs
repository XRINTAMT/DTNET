using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        public string LocalizationKey;
        bool localized;

        void Start()
        {
            /*
            TryLocalize();
            yield return new WaitForSecondsRealtime(0.2f);
            TryLocalize();
            LocalizationManager.LocalizationChanged += Localize;
            */
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        private void TryLocalize()
        {
            /*
            try
            {
                Localize();
                
                localized = true;
            }
            catch
            {
                Debug.LogError("Failed to localize, trying again!");
                GetComponent<Text>().text = "Failed to localize, trying again!";
            }
            */

        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        public void Localize()
        {
            GetComponent<Text>().text = LocalizationManager.Localize(LocalizationKey);
        }

        void Update()
        {
            /*
            if (!localized)
                TryLocalize();
            */
        }
    }
}