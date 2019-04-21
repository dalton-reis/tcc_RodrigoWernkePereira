using UnityEngine;

public class SnowController
{
    private GameObject _ground;
    private Material _groundMaterial;
    private Material _snowMaterial;
    private ParticleSystem _snowParticleSystem;

    public bool Snowing { get; set; }

    private float _snowTime = 0;
    private float _groundTime = 0;
    private float _secondsToBeCoveredInSnow = 5f;
    private float _secondsToMeltSnow = 5f;

    public SnowController()
    {
        _snowParticleSystem = GameObject.Find("SnowParticleSystem").GetComponent<ParticleSystem>();

        _ground = GameObject.Find("Terrain");

        _groundMaterial = Resources.Load<Material>("Materials/Ground/Ground") as Material;
        _snowMaterial = Resources.Load<Material>("Materials/Ground/Snow") as Material;

        Snowing = false;
    }

    public void Update(float temperature)
    {
        if (temperature <= 5f && !Snowing)
        {
            StartSnowing();
           
        }
        else if(temperature > 5f)
        {
            _groundTime = 0;

            StopSnowing();
        }

        if (Snowing)
        {
            ChangeTerrainToSnow();
            StartSnowing();
        }
        else
        {
            ChangeTerrainToGround();
            StopSnowing();
        }
    }

    void StartSnowing()
    {
        Snowing = true;
        _snowTime = 0;

        if (!_snowParticleSystem.isEmitting)
        {
            _snowParticleSystem.Play();
        }
    }

    void StopSnowing()
    {
        Snowing = false;

        if (_snowParticleSystem.isEmitting)
        {
            _snowParticleSystem.Stop();
        }
    }

    void ChangeTerrainToSnow()
    {
        var renderer = _ground.GetComponent<Renderer>();

        renderer.material.color = Color.Lerp(_groundMaterial.color, _snowMaterial.color, _groundTime);

        if (_groundTime < 1)
        {
            _groundTime += Time.deltaTime / _secondsToBeCoveredInSnow;
        }
    }

    void ChangeTerrainToGround()
    {
        var renderer = _ground.GetComponent<Renderer>();

        renderer.material.color = Color.Lerp(_snowMaterial.color, _groundMaterial.color, _snowTime);

        if (_snowTime < 1)
        {
            _snowTime += Time.deltaTime / _secondsToMeltSnow;
        }
    }
}