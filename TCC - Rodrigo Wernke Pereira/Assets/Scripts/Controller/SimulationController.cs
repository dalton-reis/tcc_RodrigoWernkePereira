using System.Collections;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    private WindController _windController;
    private CloudController _cloudController;
    private RainController _rainController;
    private WaterController _waterController;
    private TemperatureController _temperatureController;
    private SnowController _snowController;
    private TreeGrowthStateManager _treeGrowthStateManager;

    private bool _sceneRestarted;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        _windController = new WindController();
        _cloudController = new CloudController();
        _rainController = new RainController();
        _temperatureController = new TemperatureController();
        _waterController = new WaterController();
        _snowController = new SnowController();

        _treeGrowthStateManager = new TreeGrowthStateManager();
        StartCoroutine(_treeGrowthStateManager.GrowTrees());

        _sceneRestarted = false;
    }

    void Update()
    {
        _windController.Update();
        _temperatureController.Update();
        _snowController.Update(_temperatureController.Temperature);
        _cloudController.Update(_windController.WindForce, _temperatureController.Temperature);
        _rainController.Update(_cloudController.CorrectPosition);
        _waterController.Update(_temperatureController.Temperature, _rainController.Raining, _cloudController.CorrectPosition);
        //_treeGrowthStateManager.Update();

        RestartScene(_temperatureController.Temperature);
    }

    IEnumerator RestartSceneAfterSeconds()
    {
        yield return new WaitForSeconds(5);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void RestartScene(float temperature)
    {
        if (temperature > 250 && !_sceneRestarted)
        {
            RestartSceneEffects(temperature);
            StartCoroutine(RestartSceneAfterSeconds());

            _sceneRestarted = true;
        }
    }

    void RestartSceneEffects(float temperature)
    {
        var word = GameObject.FindGameObjectWithTag("Scene Target");

        var particleSystems = word.GetComponentsInChildren<ParticleSystem>();

        //para todas as particles
        foreach (var particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }

        //ativa o fogo
        var fire = GameObject.Find("Fire");

        var fireParticles = fire.GetComponentsInChildren<ParticleSystem>();

        foreach (var particleSystem in fireParticles)
        {
            particleSystem.Play();
        }

        //remove todas as trees
        var trees = GameObject.FindGameObjectsWithTag("Tree");

        foreach (var tree in trees)
        {
            tree.SetActive(false);
        }

        //remove a água
        var waters = GameObject.FindGameObjectsWithTag("Water");

        foreach (var water in waters)
        {
            water.SetActive(false);
        }
    }
}