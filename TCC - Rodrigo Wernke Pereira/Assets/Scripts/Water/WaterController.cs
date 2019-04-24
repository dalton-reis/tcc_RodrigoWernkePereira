using UnityEngine;

public class WaterController {

    private GameObject _water;

    public WaterController() {
        _water = GameObject.FindGameObjectWithTag("Water");
    }

    public void Update(float temperature, bool raining, bool cloudsCorrectPosition) {
        Evaporate(temperature, raining, cloudsCorrectPosition);
        Condense(temperature, raining,cloudsCorrectPosition);
    }

    void Evaporate(float temperature, bool raining, bool cloudsCorrectPosition) {
        if (temperature > 100f && (!raining || !cloudsCorrectPosition)) {
            _water.GetComponent<Water>().Evaporate();
        }
    }

    void Condense(float temperature, bool raining, bool cloudsCorrectPosition) {
        if (temperature < 80 && (raining && cloudsCorrectPosition)) {
            _water.GetComponent<Water>().Condense();
        }
    }
}