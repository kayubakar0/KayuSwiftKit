using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

namespace KayuSwiftKit.Core
{
    // Script from ahmadfzn
    public class KyVideoPlayer : MonoBehaviour
    {
        [Header("Video player")]
        [SerializeField] private VideoPlayer videoPlayer;

        [Header("Panel")]
        [SerializeField] private GameObject videoOverlay;
        [SerializeField] private GameObject playIcon;
        [SerializeField] private GameObject pauseIcon;
        [SerializeField] private Slider slider;
        
        [Header("Event")]
        [SerializeField] private UnityEvent onVideoEnd;
    
        //Local component
        private bool _isPlayed;
        private bool _isShowOverlay;
        private bool _isDraggingSlider;
        private Coroutine _overlayCoroutine;

        public void VideoEnd(VideoPlayer vP)
        {
            if(_overlayCoroutine != null) StopCoroutine(_overlayCoroutine);
            videoOverlay.SetActive(true);
            onVideoEnd?.Invoke();
        }
        
        public void PlayVideo()
        {
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
            videoPlayer.loopPointReached += VideoEnd;
            _isPlayed = true;
            
            videoPlayer.gameObject.SetActive(true);
    
            videoPlayer.Play();
        }
    
        private void VideoPlayer_prepareCompleted(VideoPlayer source)
        {
            slider.maxValue = 1;
            slider.minValue = 0;
            slider.value = 0;
        }
        
        private void Update()
        {
            if (videoPlayer.isPrepared && !_isDraggingSlider)
            {
                slider.value = (float)(videoPlayer.time / videoPlayer.length);
            }
        }
    
        public void PlayPauseVideo()
        {
            if(videoPlayer.isPlaying){
                _isPlayed = false;
                videoPlayer.Pause();
                StopCoroutine(_overlayCoroutine);
                _overlayCoroutine = null;
                videoOverlay.SetActive(true);
                playIcon.SetActive(true);
                pauseIcon.SetActive(false);
            }
            else{
                _isPlayed = true;
                videoPlayer.Play();
                _overlayCoroutine = StartCoroutine(OverlayIEnum(2f));
                pauseIcon.SetActive(true);
                playIcon.SetActive(false);
            }
        }
    
        public void ShowOverlay()
        {
            if(!_isPlayed) return;
    
            if(_isShowOverlay){
                if(_overlayCoroutine != null)
                {
                    StopCoroutine(_overlayCoroutine);
                    _overlayCoroutine = null;
                }
                videoOverlay.SetActive(false);
                _isShowOverlay = false;
            }
            else
            {
                _overlayCoroutine = StartCoroutine(OverlayIEnum(2.5f));
            }
        }

        private IEnumerator OverlayIEnum(float time)
        {
            videoOverlay.SetActive(true);
            _isShowOverlay = true;
    
            yield return new WaitForSeconds(time);
    
            while(_isDraggingSlider) yield return null;
    
            yield return new WaitForSeconds(2f);
    
            videoOverlay.SetActive(false);
            _isShowOverlay = false;
            StopCoroutine(_overlayCoroutine);
            _overlayCoroutine = null;
        }
    
        public void OnSliderDrag()
        {
            _isDraggingSlider = true;
        }
    
        public void OnSliderRelease()
        {
            videoPlayer.time = (long)(slider.value * videoPlayer.length);
            slider.value = slider.value = (float)(videoPlayer.time / videoPlayer.length);
            _isDraggingSlider = false;
        }
    }
}
