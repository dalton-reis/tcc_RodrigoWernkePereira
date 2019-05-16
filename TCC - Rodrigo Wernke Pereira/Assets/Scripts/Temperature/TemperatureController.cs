using System;
using System.Collections;
using UnityEngine;

public class TemperatureController
{
    public float Temperature { get; set; }
    public float TargetTemperature { get; set; }

    private Func<IEnumerator, Coroutine> _startCoroutine;
    private GameObject _temperatureTarget;
    private TemperatureTextManager _temperatureTextManager;
    private DayNightCycle DayNightCycle;
    private WaitForSeconds _waitForSeconds;
    private float _lastTempFromTarget;

    public TemperatureController(Func<IEnumerator, Coroutine> StartCoroutine)
    {
        DayNightCycle = GameObject.Find("Sun And Moon Rotator").GetComponent<DayNightCycle>();

        _startCoroutine = StartCoroutine;
        _temperatureTarget = GameObject.FindGameObjectWithTag("Temperature Target");
        _temperatureTextManager = new TemperatureTextManager();
        _waitForSeconds = new WaitForSeconds(1f);
        _lastTempFromTarget = 0f;

        _startCoroutine.Invoke(UpdateTemperatureRelativeToTimeOfDay());
    }

    public void Update()
    {
        UpdateTemperature();

        _temperatureTextManager.Update(Temperature);
    }

    private void UpdateTemperature()
    {
        bool isBeingTracked = VuforiaTools.IsBeingTracked("Temperature Target");

        var temperatureTargetTransform = _temperatureTarget.GetComponentInChildren<Transform>();

        var targetAngle = temperatureTargetTransform.localRotation.eulerAngles.y;

        if (isBeingTracked)
        {
            if (_lastTempFromTarget != Map(targetAngle, 0, 350, 0, 51)
                && temperatureTargetTransform.localRotation.eulerAngles.y < 350)
            {
                Temperature = Map(targetAngle, 0, 360, 0, 51);
                TargetTemperature = Temperature;

                _lastTempFromTarget = Temperature;

                _temperatureTextManager.UpdateTargetText(Temperature);
            }
        }
    }

    public IEnumerator UpdateTemperatureRelativeToTimeOfDay()
    {
        while (true)
        {
            yield return _waitForSeconds;

            LowerTempDuringNight();
            IncreaseTemperatureDuringDay();
        }
    }

    private void LowerTempDuringNight()
    {
        if (DayNightCycle.IsNight)
        {
            Temperature -= .5f;
        }
    }

    private void IncreaseTemperatureDuringDay()
    {
        if (DayNightCycle.IsDay)
        {
            Temperature += .5f;
        }
    }

    public float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}