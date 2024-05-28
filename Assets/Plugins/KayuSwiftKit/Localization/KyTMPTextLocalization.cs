using KayuSwiftKit.Core;
using TMPro;
using UnityEngine;

namespace KayuSwiftKit.Localization
{
    // This script is useful for overriding tmp text for easy implementing localization
    public class KyTMPTextLocalization : MonoBehaviour
    {
        [SerializeField] private LanguageData textData;
        private TMP_Text _text;
        
        private void Start()
        {
            if (TryGetComponent(out TMP_Text tmpText))
            {
                _text = tmpText;
                _text.text = textData.GetText();
            }
            else
            {
                KyDebugLogger.LogDebugColor("This GameObject Dont have TMP Text Component", Color.red);
            }
        }

        public void LoadData()
        {
            _text.text = textData.GetText();
        }
    }
}
