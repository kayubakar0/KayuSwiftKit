using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace KayuSwiftKit.Canvas_UI
{
    public class KySimpleStepItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text stepText;
        
        [SerializeField] private Image outerCircle;
        [SerializeField] private Image checklistImage;

        public void Initialize(string content)
        {
            stepText.text = content;
        }

        public void ShowUnselectedStep()
        {
            stepText.color = Color.gray;
            outerCircle.color = Color.gray;
            checklistImage.gameObject.SetActive(false);
        }

        public void ShowSelectedStep()
        {
            stepText.color = Color.white;
            outerCircle.color = Color.white;
        }

        public void ShowCompletedStep()
        {
            stepText.color = Color.green;
            outerCircle.color = Color.green;
            checklistImage.gameObject.SetActive(true);
            checklistImage.color = Color.green;
        }



    }
}
