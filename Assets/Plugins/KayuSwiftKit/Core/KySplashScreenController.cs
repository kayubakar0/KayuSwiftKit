using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KayuSwiftKit.Core
{
    public class KySplashScreenController : MonoBehaviour
    {
        //Game Object  
        [SerializeField] private GameObject[] listPanelImage;  
     
        //Delay Time  
        [Tooltip("Delay fade to next panel")]
        [SerializeField] private float delayTime;  
    
        //Count array  
        private int _listCanvasLenght;  
        private int _currentIndex;
        
        //Event
        [SerializeField] private UnityEvent onDoneLoadSPlashScreen;
        

        void Start()  
        {        
            _listCanvasLenght = listPanelImage.Length;

            if (listPanelImage == null) return;
            
            StartCoroutine(StartSplashScreen());  
        } 
     
        private IEnumerator StartSplashScreen()  
        {        
            for (int i = 0; i < listPanelImage.Length; i++)  
            {            
                if (_currentIndex == _listCanvasLenght - 1)  
                {                
                    yield return new WaitForSeconds(delayTime);  
                    
                    //End image splash screen
                    onDoneLoadSPlashScreen?.Invoke();
                }            
                else  
                {  
                    yield return new WaitForSeconds(delayTime);  
                    listPanelImage[i].SetActive(false);  
                    listPanelImage[i+1].SetActive(true);  
                    _currentIndex++;  
                }        
            }    
        }
    }
}
