using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KayuSwiftKit.Core
{
    public class KyChangeSceneLoopType : MonoBehaviour
    {

        [SerializeField] private GameObject panelLoopLoading;

        private void Start()
        {
            panelLoopLoading.SetActive(false);
        }

        //Load Scene  
        public void LoadSceneAsync(string sceneName)  
        {        
            StartCoroutine(LoadScene(sceneName));  
        }    
    
        IEnumerator LoadScene(string sceneName)
        {
            //Activate panel loop loading
            panelLoopLoading.SetActive(true);
            
            yield return new WaitForSeconds(1f);
            AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName);  
            asyncScene.allowSceneActivation = false;  
        
            while (asyncScene.isDone)  
            {            
                yield return new WaitForEndOfFrame();  
            }  
              
            asyncScene.allowSceneActivation = true;  
        }    
    }
}
