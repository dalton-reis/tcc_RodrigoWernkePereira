using UnityEngine;

public class SimulationController : MonoBehaviour
{
    private WindController _windController;

    void Start()
    {
        _windController = new WindController();
    }

    // Update is called once per frame
    void Update()
    {
        _windController.UpdateForces();
    }
}
