using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KayuSwiftKit.Canvas_UI
{
    public class KySimpleStepManager : MonoBehaviour
    {
        [SerializeField] private GameObject stepPrefab;
        [SerializeField] private Transform stepSpawn;
        [SerializeField] private List<Step> instructionList;
        [SerializeField] private GameObject finishUI;
        
        private int currentInstructionIndex;

        private List<GameObject> stepGameObjects;

        private void ShowStep()
        {
            for (int i = 0; i < instructionList.Count; i++)
            {
                GameObject stepGo = Instantiate(stepPrefab, stepSpawn);

                KySimpleStepItem item = stepGo.GetComponent<KySimpleStepItem>();
                
                item.Initialize(instructionList[i].stepText);
                instructionList[i].SetStepItem(item);
                
                stepGameObjects.Add(stepGo);
            }
        }

        public void NextStep()
        {
            currentInstructionIndex++;

            if (currentInstructionIndex > 0)
            {
                instructionList[currentInstructionIndex - 1].StepCompleted();
            }

            if (currentInstructionIndex == instructionList.Count)
            {
                finishUI.SetActive(true);
                return;
            }
            
            instructionList[currentInstructionIndex].StepStart();
        }
        
    }

    [Serializable]
    public class Step
    {
        public string stepText;
        
        [SerializeField] private UnityEvent OnStepStarted;
        [SerializeField] private UnityEvent OnStepCompleted;

        private KySimpleStepItem _stepItem;

        public void SetStepItem(KySimpleStepItem stepItem)
        {
            _stepItem = stepItem;
        }

        public void StepStart()
        {
            _stepItem.ShowSelectedStep();
            OnStepStarted?.Invoke();
        }

        public void StepCompleted()
        {
            _stepItem.ShowCompletedStep();
            OnStepCompleted?.Invoke();
        }
    }
}
