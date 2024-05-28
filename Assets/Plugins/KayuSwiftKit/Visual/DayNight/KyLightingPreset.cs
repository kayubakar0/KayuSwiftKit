using UnityEngine;

namespace KayuSwiftKit.Visual
{
    [CreateAssetMenu(fileName = "DayNight Light Preset", menuName = "KyScribtables/DayNight Light Preset", order = 1)]
    public class KyLightingPreset : ScriptableObject
    {
        public Gradient ambientColor;
        public Gradient directionalColor;
        public Gradient fogColor;


    }
}
