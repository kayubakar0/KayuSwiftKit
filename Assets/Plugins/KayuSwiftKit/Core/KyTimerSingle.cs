using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KayuSwiftKit.Core
{
    public class KyTimerSingle : MonoBehaviour
    {
        public TMP_Text timerText;  // Attach your Text component here

        public static KyTimerSingle instance;
        private float startTime;
        private float elapsedTime;
        private bool isPaused;

        private void Awake()
        {
            // Implementing Singleton pattern
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void Start()
        {
            StartTimer();
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
        }

        void Update()
        {
            if (!isPaused)
            {
                elapsedTime = Time.time - startTime;
                UpdateTimerText();
            }
        }

        void UpdateTimerText()
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (timerText != null)
            {
                timerText.text = timeString;
            }
        }

        public void StartTimer()
        {
            startTime = Time.time - elapsedTime;
            isPaused = false;
        }

        public void PauseTimer()
        {
            isPaused = true;
        }

        public void ResumeTimer()
        {
            isPaused = false;
            startTime = Time.time - elapsedTime;
        }

        public void StopAndSaveTimer()
        {
            isPaused = true;
            // Save elapsed time or perform other actions
            
        }

        public void ResetTimer()
        {
            startTime = Time.time;
            elapsedTime = 0f;
            isPaused = false;
            UpdateTimerText();
        }

        // Callback method for the scene loaded event
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (instance != null && instance == this)
            {
                // Destroy the singleton instance when a new scene is loaded
                Destroy(this.gameObject);
            }
        }
    }
}
