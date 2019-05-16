using System;
using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    public bool IsNight { get; private set; }
    public bool IsDay { get; private set; }
    public float DayLengthInSeconds;
    public GameObject DayTimeGameObject;
    public int TimeOfDay;

    private float _rotationAngle = 0;
    private double _rotationPercentage = 0;
    private int day = 0;

    private void Update() {

        float angle = transform.eulerAngles.z;

        transform.Rotate(0, 0, DegreeInSeconds(DayLengthInSeconds) * Time.deltaTime);

        _rotationAngle += DegreeInSeconds(DayLengthInSeconds) * Time.deltaTime;

        _rotationPercentage = ((_rotationAngle / 360) * -1);
        
        if (_rotationAngle < -360) {
            _rotationAngle = 0;
            day++;
        }

        TimeOfTheDay();
    }

    private double ConvertRange(int originalStart, int originalEnd, int newStart, int newEnd, double value) {
        double scale = (newEnd - newStart) / (originalEnd - originalStart);
        return (newStart + ((value - originalStart) * scale));
    }

    private void TimeOfTheDay() {

        double decimalTime = ConvertRange(0, 1, 0, 24, _rotationPercentage);

        int hour = (int)(decimalTime);
               
        int min = (int)((decimalTime - Math.Truncate(decimalTime)) * 60);

        DayTimeGameObject.GetComponent<TextMeshProUGUI>().text = "Day: " + day + ". " + hour + ":" + min;

        TimeOfDay = hour;

        if (TimeOfDay > 18 || TimeOfDay < 6)
        {
            IsNight = true;
            IsDay = false;
        }
        else
        {
            IsNight = false;
            IsDay = true;
        }

    }

    private float DegreeInSeconds(float seconds) {
        return -360 / seconds;
    }
}