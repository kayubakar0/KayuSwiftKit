using UnityEngine;
using UnityEngine.Events;

namespace Plugins.KayuSwiftKit.Core
{
    public class KyOnTriggerGlobal : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTriggerEnter;  
        [SerializeField] private UnityEvent onTriggerExit;  
        [SerializeField] private string tagTrigger; 
     
        private void OnTriggerEnter(Collider other)  
        {        
            if (other.CompareTag(tagTrigger))  
            {      
                onTriggerEnter?.Invoke();
            }
        }    
    
        private void OnTriggerExit(Collider other)  
        {
            if (other.CompareTag(tagTrigger))
            {
                onTriggerExit?.Invoke();
            }
        }
    }
}
