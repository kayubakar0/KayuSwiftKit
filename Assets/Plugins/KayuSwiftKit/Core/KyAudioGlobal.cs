using System.Collections.Generic;
using UnityEngine;

namespace KayuSwiftKit.Core
{
    public class KyAudioGlobal : MonoBehaviour
    {
        private static KyAudioGlobal instance;  
        private AudioSource audioSource;  
    
        private void Awake()  
        {        
	        if (instance != null && instance != this)  
            {            
	            Destroy(gameObject);  
                return;  
            }  
            instance = this;  
            DontDestroyOnLoad(gameObject);  
    
            audioSource = GetComponent<AudioSource>();  
        }  
        
        public static KyAudioGlobal Instance  
        {  
            get  
            {  
                if (instance == null)  
                {                
	                instance = FindObjectOfType<KyAudioGlobal>();  
                    if (instance == null)  
                    {                    
	                    GameObject manager = new GameObject("AudioGlobal");  
                        instance = manager.AddComponent<KyAudioGlobal>();  
                    }            
                }  
                return instance;  
            }    
        }  
        
        [Header("Global Audio")] public List<AudioData> audioList;  
        private Dictionary<string, AudioClip> audioDictionary;  
    
        private void Start()  
        {        
	        //Global Audio  
            audioDictionary = new Dictionary<string, AudioClip>();  
            foreach (AudioData audioData in audioList)  
            {            
	            if (!audioDictionary.ContainsKey(audioData.clipName))  
                {                
	                audioDictionary.Add(audioData.clipName, audioData.audioClip);  
                }            
	            else  
                {  
                    Debug.LogWarning("Duplicate audio clip name: " + audioData.clipName);  
                }        
            }  
        }  
    
        //============================Play Audio=============================  
        //Global Audio    
        public void PlayAudioClip(string clipName)  
        {        
	        if (!audioDictionary.ContainsKey(clipName))  
            {            
                return;  
            } 
             
            AudioClip audioClip = audioDictionary[clipName];  
            audioSource.clip = audioClip;  
            audioSource.loop = false;  
            audioSource.Play();  
        }  
        
        //stop audio  
        public void StopAudioClip()  
        {        
	        audioSource.Stop();  
        }  
         
        //loop audio  
        public void LoopAudioClip(string clipName)  
        {        
	        if (!audioDictionary.ContainsKey(clipName))  
            {            
                return;  
            } 
             
            AudioClip audioClip = audioDictionary[clipName];  
            audioSource.clip = audioClip;  
            audioSource.loop = true;  
            audioSource.Play();  
        } 
         
        //non active loop audio  
        public void NonActiveLoop()  
        {        
	        audioSource.loop = false;  
        }
    }
    
    [System.Serializable]  
    public struct AudioData  
    {  
        public string clipName;  
        public AudioClip audioClip;  
    }  
}
