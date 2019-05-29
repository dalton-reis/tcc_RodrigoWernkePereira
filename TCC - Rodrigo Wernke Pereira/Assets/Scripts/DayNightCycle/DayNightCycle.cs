using System;
using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public bool IsNight { get; private set; }
    public bool IsDay { get; private set; }
    public float DayLengthInSeconds;

    public GameObject DayInputPanel;
    public GameObject TimeInputPanel;

    private float _rotationAngle;
    private double _rotationPercentage;
    private int _day;
    private int _hour;
    private int _minute;

    private void Start()
    {
        _rotationAngle = 0;
        _rotationPercentage = 0;
        _day = 1;
        _hour = 0;
        _minute = 0;
    }

    private void Update()
    {
        transform.Rotate(0, 0, DegreeInSeconds(DayLengthInSeconds) * Time.deltaTime);

        _rotationAngle += DegreeInSeconds(DayLengthInSeconds) * Time.deltaTime;

        _rotationPercentage = ((_rotationAngle / 360) * -1);

        if (_rotationAngle < -360)
        {
            _rotationAngle = 0;
            _day++;
        }

        TimeOfTheDay();
        UpdateTextDisplays();
    }

    private double ConvertRange(int originalStart, int originalEnd, int newStart, int newEnd, double value)
    {
        double scale = (newEnd - newStart) / (originalEnd - originalStart);
        return (newStart + ((value - originalStart) * scale));
    }

    private void TimeOfTheDay()
    {
        double decimalTime = ConvertRange(0, 1, 0, 24, _rotationPercentage);

        _hour = (int)(decimalTime);

        _minute = (int)((decimalTime - Math.Truncate(decimalTime)) * 60);

        if ((_hour >= 18) || (_hour < 6))
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

    private float DegreeInSeconds(float seconds)
    {
        return -360 / seconds;
    }

    private void UpdateTextDisplays()
    {
        DayInputPanel.GetComponent<TextMeshProUGUI>().text = $"{_day}";
        TimeInputPanel.GetComponent<TextMeshProUGUI>().text = $"{_hour}:{_minute}";
    }
}