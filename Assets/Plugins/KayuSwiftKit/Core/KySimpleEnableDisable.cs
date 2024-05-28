using UnityEngine;

namespace KayuSwiftKit.Core
{
    public class KySimpleEnableDisable : MonoBehaviour
    {
        [SerializeField] private GameObject[] firstGroupPanel;
        [SerializeField] private GameObject[] secondGroupPanel;

        public void DelayEnableFirstGroub(float timeDelay)
        {
            Invoke(nameof(EnableFirstGroub), timeDelay);
        }
        
        public void EnableFirstGroub()
        {
            //Check null
            if (firstGroupPanel == null || secondGroupPanel == null) return;
            
            foreach (var firstPanel in firstGroupPanel)  
            {                
                firstPanel.SetActive(true);  
            }  
            
            foreach (var secondPanel in secondGroupPanel)  
            {                
                secondPanel.SetActive(false);  
            }
        }

        public void DelayEnableSecondGroub(float timeDelay)
        {
            Invoke(nameof(EnableSecondGroub), timeDelay);
        }
    
        public void EnableSecondGroub()
        {
            //Check null
            if (firstGroupPanel == null || secondGroupPanel == null) return;
            
            foreach (var firstPanel in firstGroupPanel)  
            {                
                firstPanel.SetActive(false);  
            }
              
            foreach (var secondPanel in secondGroupPanel)  
            {                
                secondPanel.SetActive(true);  
            }
        }
    }
}
