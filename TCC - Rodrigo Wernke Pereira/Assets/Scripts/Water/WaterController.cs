using TMPro;
using UnityEngine;

public class WaterController {

    private GameObject _water;

    private float evaporationSpeed = 0.03f * Time.deltaTime;
    private float condensationSpeed = 0.03f * Time.deltaTime;

    private Vector3 _waterEvaporationPosition;

    private Vector3 _waterNormalPosition;

    public WaterController() {
        _water = GameObject.FindGameObjectWithTag("Water");

        _waterNormalPosition = _water.transform.localPosition;

        _waterEvaporationPosition = _water.transform.localPosition;

        _waterEvaporationPosition.y -= 0.13f;
    }

    public void Update(float temperature, bool raining, bool cloudsCorrectPosition) {
        Evaporate(temperature, raining, cloudsCorrectPosition);
        Condense(temperature, raining,cloudsCorrectPosition);
    }

    private void Evaporate(float temperature, bool raining, bool cloudsCorrectPosition) {
        if (temperature > 100f && (!raining || !cloudsCorrectPosition)) {
            _water.transform.localPosition = Vector3.MoveTowards(_water.transform.localPosition, _waterEvaporationPosition, evaporationSpeed);
        }
    }

    private void Condense(float temperature, bool raining, bool cloudsCorrectPosition) {

        VuforiaTools.AddTextToDebugger("Raining: " + raining + "\nClouds: " + cloudsCorrectPosition);

        if (temperature < 80 && (raining && cloudsCorrectPosition)) {
            _water.transform.localPosition = Vector3.MoveTowards(_water.transform.localPosition, _waterNormalPosition, condensationSpeed);
        }
    }
}