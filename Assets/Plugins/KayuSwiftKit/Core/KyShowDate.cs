using System;
using TMPro;
using UnityEngine;

namespace Plugins.KayuSwiftKit.Core
{
    public class KyShowDate : MonoBehaviour
    {
        // Text field to display the current date
        [Header("Date TMP_Text"),SerializeField] private TMP_Text currentDateText;
        // Text field to display the date of next week
        [SerializeField] private TMP_Text nextWeekDateText;
        
        //the distance between the current and next dates
        [Header("DistanceToNextDate"), SerializeField] private int distanceDate;

        void Start()
        {
            // Get the current date
            DateTime currentDate = DateTime.Now;

            if (currentDateText != null)
            {
                // Display the current date in the UI Text currentDate
                currentDateText.text = currentDate.ToString("dd MMMM yyyy");
            }

            // Get the date of next week
            DateTime nextWeekDate = currentDate.AddDays(distanceDate);

            if (nextWeekDateText != null)
            {
                // Display the date of next week in the UI Text nextWeekDate
                nextWeekDateText.text = nextWeekDate.ToString("dd MMMM yyyy");
            }
        }
    }
}
