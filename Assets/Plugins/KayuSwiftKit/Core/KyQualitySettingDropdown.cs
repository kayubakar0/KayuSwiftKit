using TMPro;
using UnityEngine;

namespace Plugins.KayuSwiftKit.Core
{
    public class KyQualitySettingDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown qualityDropdown;

        private void Start()
        {
            switch (PlayerPrefs.GetInt("QualityLevel", 1))
            {
                case 0:
                    QualitySettings.SetQualityLevel(0);
                    qualityDropdown.value = 0;
                    break;
                case 1:
                    QualitySettings.SetQualityLevel(1);
                    qualityDropdown.value = 1;
                    break;
                case 2:
                    QualitySettings.SetQualityLevel(2);
                    qualityDropdown.value = 2;
                    break;
            }
            
            //Add listener to dropdown
            qualityDropdown.onValueChanged.AddListener(args => DropdownValueChanged(qualityDropdown));
        }
    
        public void DropdownValueChanged(TMP_Dropdown change)
        {
            QualitySettings.SetQualityLevel(change.value);
            PlayerPrefs.SetInt("QualityLevel", change.value);

            LogQualitySetting();
        }

        private void LogQualitySetting()
        {
            // Get the current quality level index
            int currentQualityLevel = QualitySettings.GetQualityLevel();

            // Log the current quality level
            KyDebugLogger.LogDebugColor("Current Quality Level: " + currentQualityLevel, Color.white);

            // Optionally, log the name of the current quality level
            string currentQualityLevelName = QualitySettings.names[currentQualityLevel];
            KyDebugLogger.LogDebugColor("Current Quality Level Name: " + currentQualityLevelName, Color.white);
        }
    }
}
