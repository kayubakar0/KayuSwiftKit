using UnityEngine;

namespace KayuSwiftKit.Localization
{
    public class KyLanguageData : MonoBehaviour
    {
        /*
         Note : Currently it only supports switching between two languages
         
         How to use Language Data
         1. Change the string that can be displayed to the inspector to be language data 
         so that in the inspector you can fill in text in two different languages.
         
         2. To call data you can use variable name.GetText()
         
         3. To change Language you must using PlayerPrefs.SetInt("Language", ...) 
         The triple point can be replaced with the integer 0 for the first language and 
         the integer 1 for the second language
         */
    }
    
    [System.Serializable]
    public class LanguageData
    {
        //Key Player Prefs
        private const string LanguageKey = "Language";
        
        [SerializeField] 
        [TextArea(4, 10)]
        private string language1;
    
        [SerializeField] 
        [TextArea(4, 10)]
        private string language2; 
 
        public string GetText(){ 
            return PlayerPrefs.GetInt(LanguageKey, 0) == 0 ? language1 : language2; 
        } 
    }
}
