using UnityEngine;

public class SimulationController : MonoBehaviour {

    private WindController _windController;
    private CloudController _cloudController;
    private RainController _rainController;
    private WaterController _waterController;
    private TemperatureController _temperatureController;

    void Start() {
        Screen.orientation = ScreenOrientation.LandscapeRight;

        _windController = new WindController();
        _cloudController = new CloudController();
        _rainController = new RainController();
        _temperatureController = new TemperatureController();
        _waterController = new WaterController();
    }

    void Update() {
        _windController.UpdateForces();
        _cloudController.UpdateClouds((float)_windController.WindForce, _temperatureController.Temperature);
        _rainController.UpdateRain(_cloudController.CorrectPosition);
        _temperatureController.UpdateTemperature(_cloudController.ActiveClouds);
        _waterController.UpdateWater(_temperatureController.Temperature, _rainController.Raining, _cloudController.CorrectPosition);
    }
}