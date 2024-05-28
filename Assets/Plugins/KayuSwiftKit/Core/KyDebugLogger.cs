using UnityEngine;

namespace KayuSwiftKit.Core
{
    public static class KyDebugLogger
    {
        public static void LogDebugColor(string message, Color color)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(color);
            string coloredMessage = $"<color=#{hexColor}>{message}</color>";

            Debug.Log(coloredMessage);
        }
    }
}
