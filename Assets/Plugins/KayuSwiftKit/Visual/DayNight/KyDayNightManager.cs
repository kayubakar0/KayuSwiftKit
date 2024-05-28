using System;
using UnityEngine;

namespace KayuSwiftKit.Visual
{
    [ExecuteAlways]
    public class KyDayNightManager : MonoBehaviour
    {
        [SerializeField] private Light directionalLight;
        [SerializeField] private KyLightingPreset preset;

        [SerializeField, Range(0, 24)] private float timeOfDay;
        [SerializeField] private float dayTimeMultiplier = 1f;  // Multiplier untuk siang hari
        [SerializeField] private float nightTimeMultiplier = 2f;

        private void Update()
        {
            if (preset == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                float currentMultiplier = (timeOfDay >= 6f && timeOfDay < 18f) ? dayTimeMultiplier : nightTimeMultiplier;

                timeOfDay += Time.deltaTime * currentMultiplier;
                timeOfDay %= 24;
                UpdateLighting(timeOfDay/24);
            }
        }

        private void UpdateLighting(float timePercent)
        {
            RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
            RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

            if (directionalLight != null)
            {
                directionalLight.color = preset.directionalColor.Evaluate(timePercent);
                directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360F) - 90f, 170f, 0));
            }
        }


        private void OnValidate()
        {
            if (directionalLight != null)
            {
                return;
            }

            if (RenderSettings.sun != null)
            {
                directionalLight = RenderSettings.sun;
            }
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (var light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        directionalLight = light;
                        return;
                    }
                }
            }
        }
    }
}
