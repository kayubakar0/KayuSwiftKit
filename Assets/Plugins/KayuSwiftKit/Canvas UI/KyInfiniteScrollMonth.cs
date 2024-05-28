using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KayuSwiftKit.Canvas_UI
{
    public class KyInfiniteScrollMonth : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform viewPortTransform;
        [SerializeField] private RectTransform contentPanelTransform;
        [SerializeField] private VerticalLayoutGroup VLG;
    
        [Header("Spawn Header")]
        [SerializeField] private GameObject monthPrefab;
        private float _monthHeight;
        private readonly int _totalMonths = 12;
    
        private List<RectTransform> _itemList = new List<RectTransform>();
    
        // Smooth scrolling
        Vector2 OldVelocity;
        bool isUpdated;

        private void Awake()
        {
            SpawnMonths();
        }
    
        void Start()
        {
            isUpdated = false;
            OldVelocity = Vector2.zero;
            int ItemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.height / (_itemList[0].rect.height + VLG.spacing));

            for (int i = 0; i < ItemsToAdd; i++)
            {
                RectTransform RT = Instantiate(_itemList[i % _itemList.Count], contentPanelTransform);
                RT.SetAsLastSibling();
            }

            for (int i = 0; i < ItemsToAdd; i++)
            {
                int num = _itemList.Count - i - 1;
                while (num < 0)
                {
                    num += _itemList.Count;
                }
                RectTransform RT = Instantiate(_itemList[num], contentPanelTransform);
                RT.SetAsFirstSibling();
            }

            contentPanelTransform.localPosition = new Vector3(contentPanelTransform.localPosition.x,
                (0 - (_itemList[0].rect.height + VLG.spacing) * ItemsToAdd),
                contentPanelTransform.localPosition.z);
        }

        // Update is called once per frame
        void Update()
        {
            if (isUpdated)
            {
                isUpdated = false;
                scrollRect.velocity = OldVelocity;
            }

            if (contentPanelTransform.localPosition.y < 0)
            {
                Canvas.ForceUpdateCanvases();
                OldVelocity = scrollRect.velocity;
                contentPanelTransform.localPosition += new Vector3(0, _itemList.Count * (_itemList[0].rect.height + VLG.spacing), 0);
                isUpdated = true;
            }

            if (contentPanelTransform.localPosition.y > (_itemList.Count * (_itemList[0].rect.height + VLG.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                OldVelocity = scrollRect.velocity;
                contentPanelTransform.localPosition -= new Vector3( 0, _itemList.Count * (_itemList[0].rect.height + VLG.spacing), 0);
                isUpdated = true;
            }
        }
    
        void SpawnMonths()
        {
            for (int i = 0; i < _totalMonths; i++)
            {
                int monthIndex = i % 12;
                GameObject month = Instantiate(monthPrefab, contentPanelTransform);
                RectTransform rect = month.GetComponent<RectTransform>();
                _itemList.Add(rect);
            
                rect.anchoredPosition = new Vector2(0, -i * _monthHeight);
                TMP_Text text = month.GetComponentInChildren<TMP_Text>();
                text.text = GetMonthName(monthIndex);
            }
        }

        string GetMonthName(int monthIndex)
        {
            switch (monthIndex)
            {
                case 0:
                    return "Januari";
                case 1:
                    return "Februari";
                case 2:
                    return "Maret";
                case 3:
                    return "April";
                case 4:
                    return "Mei";
                case 5:
                    return "Juni";
                case 6:
                    return "Juli";
                case 7:
                    return "Agustus";
                case 8:
                    return "September";
                case 9:
                    return "Oktober";
                case 10:
                    return "November";
                case 11:
                    return "Desember";
                default:
                    return "";
            }
        }
    }
}
