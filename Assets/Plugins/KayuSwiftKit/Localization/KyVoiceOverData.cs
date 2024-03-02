using UnityEngine;
using System;

namespace Plugins.KayuSwiftKit.Localization
{
    public class KyVoiceOverData : MonoBehaviour
    {
        /*
         Note : Currently it only supports switching between two languages

         How to use Language Data
         1. Change the string that can be displayed to the inspector to be language data
         so that in the inspector you can fill in text in two different languages.

         2. To call data you can use variable name.GetAudio()

         3. To change Language you must using PlayerPrefs.SetInt("Language", ...)
         The triple point can be replaced with the integer 0 for the first language and
         the integer 1 for the second language
         */
    }
    
    [Serializable]
    public class VoiceOverData
    {
        //Key Player Prefs
        private const string LanguageKey = "Language";
        
        [SerializeField] private AudioClip audioLanguage1;
        [SerializeField] private AudioClip audioLanguage2;
        
        public AudioClip GetAudio(){ 
            return PlayerPrefs.GetInt(LanguageKey, 0) == 0 ? audioLanguage1 : audioLanguage2; 
        }
    }
}
