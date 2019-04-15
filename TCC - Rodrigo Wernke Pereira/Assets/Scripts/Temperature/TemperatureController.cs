using UnityEngine;

public class TemperatureController {
    private GameObject _temperatureTarget;

    public float Temperature { get; set; }

    public TemperatureController() {
        _temperatureTarget = GameObject.FindGameObjectWithTag("Temperature Target");

        Temperature = _temperatureTarget.transform.localRotation.eulerAngles.y;
    }

    public void UpdateTemperature(bool activeClouds) {
        Temperature = _temperatureTarget.transform.localRotation.eulerAngles.y;
    }
}