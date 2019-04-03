using UnityEngine;

public class SimulationController : MonoBehaviour
{
    private WindController _windController;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;

        _windController = new WindController();
    }

    void Update()
    {
        _windController.UpdateForces();
    }
}
