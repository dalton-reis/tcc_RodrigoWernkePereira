using UnityEngine;

public class TemperatureController{

    private GameObject _temperatureTarget;
    private TemperatureTextManager _temperatureTextManager;

    public float Temperature { get; set; }

    public TemperatureController() {
        _temperatureTarget = GameObject.FindGameObjectWithTag("Temperature Target");

        _temperatureTextManager = new TemperatureTextManager();
    }

    public void Update() {
        UpdateTemperature();

        _temperatureTextManager.Update(Temperature);
    }

    private void UpdateTemperature()
    {
        bool isBeingTracked = VuforiaTools.IsBeingTracked("Temperature Target");

        var temperatureTargetTransform = _temperatureTarget.GetComponentInChildren<Transform>();

        if (temperatureTargetTransform.localRotation.eulerAngles.y <= 280 && temperatureTargetTransform.localRotation.eulerAngles.y >= 0 && isBeingTracked)
        {
            Temperature = temperatureTargetTransform.localRotation.eulerAngles.y;
        }
    }
}