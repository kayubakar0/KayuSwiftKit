using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Plugins.KayuSwiftKit.Core
{
    public class KyAsyncChangeScene : MonoBehaviour
    {
        [Tooltip("The panel where the loading fill image is located")]
        [SerializeField] private GameObject loadingPanel;
        [Tooltip("fill image ")]
        [SerializeField] private Image loadingImage;
        
        private readonly float _delayBeforeLoading = 0.5f;

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            yield return new WaitForSeconds(_delayBeforeLoading);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                loadingPanel.SetActive(true);
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                loadingImage.fillAmount = progress;

                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }

        public void ChangeSceneAsync(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }
    }
}
