using UnityEngine;

public class WindController
{
    public float WindForce { get; set; }

    private GameObject _windTarget;
    private Transform _windTargetTransform;
    private Material _treeBranches;
    private GameObject[] _trees;
    private WindTextManager _windTextManager;
    private ParticleSystem _snowParticleSystem;
    private ParticleSystem[] _rainParticleSystems;

    private float _initialTreeSwaySpeed;
    private float _lastWindForceFromTarget;

    public WindController()
    {
        _windTextManager = new WindTextManager();
        _windTextManager.UpdatePanelText(0f);

        _initialTreeSwaySpeed = 3f;

        _windTarget = GameObject.Find("Wind Target");
        _windTargetTransform = _windTarget.transform;
        _snowParticleSystem = GameObject.Find("SnowParticleSystem").GetComponent<ParticleSystem>();
        _rainParticleSystems = GameObject.Find("Clouds").GetComponentsInChildren<ParticleSystem>();

        _lastWindForceFromTarget = 0f;

        WindForce = 0;

        SetInitialTreeSwaySpeed();
    }

    public void Update()
    {
        UpdateWindForce();

        UpdateTextDisplays();

        UpdateTreeForces();

        UpdateRainForces();

        UpdateSnowForces();
    }

    private void SetInitialTreeSwaySpeed()
    {
        _trees = GameObject.FindGameObjectsWithTag("Tree");

        foreach (var tree in _trees)
        {
            var material = tree.GetComponentInChildren<MeshRenderer>().materials[1];

            material.SetFloat("_tree_sway_speed", 3f);
        }
    }

    private void UpdateTextDisplays()
    {
        if (WindForce != _lastWindForceFromTarget)
        {
            _windTextManager.UpdatePanelText(WindForce);
            _windTextManager.UpdateTargetText(WindForce);
        }
    }

    private void UpdateWindForce()
    {
        bool isBeingTracked = VuforiaTools.IsBeingTracked("Wind Target");

        if (isBeingTracked)
        {
            var targetAngle = _windTargetTransform.localRotation.eulerAngles.y;

            var mappedAngle = Map(targetAngle, 0, 280, 0, 50);

            if (_lastWindForceFromTarget != mappedAngle 
                && _windTargetTransform.localRotation.eulerAngles.y <= 280)
            {
                WindForce = mappedAngle;
                _lastWindForceFromTarget = WindForce;

                _windTextManager.UpdatePanelText(WindForce);
                _windTextManager.UpdateTargetText(WindForce);
            }
        }
    }

    private void UpdateTreeForces()
    {
        foreach (var tree in _trees)
        {
            if (tree.activeSelf && (tree.GetComponent<Tree>().TreeGrowthState == TreeGrowthState.Mature || tree.GetComponent<Tree>().TreeGrowthState == TreeGrowthState.Snag))
            {
                //módulo de forceOverTime das animações de folhas caindo
                var particleSystemForceOverTimeModule = tree.GetComponentInChildren<ParticleSystem>().forceOverLifetime;

                //adiciona força nas folhas
                if (WindForce > 0 && _windTarget.transform.rotation.y < 0)
                {
                    WindForce = 0;
                }

                particleSystemForceOverTimeModule.z = (int)WindForce / 10;

                //balanço dos galhos das árvores
                var material = tree.GetComponentInChildren<MeshRenderer>().materials[1];

                if (WindForce > _initialTreeSwaySpeed && (WindForce % 10 == 0) && WindForce != 0)
                {
                    material.SetFloat("_tree_sway_speed", (int)WindForce / 10);
                }
            }
        }
    }

    private void UpdateRainForces()
    {
        foreach (var rainParticleSystem in _rainParticleSystems)
        {
            if (rainParticleSystem.isPlaying)
            {
                var forceOverLifeTimeModule = rainParticleSystem.forceOverLifetime;

                forceOverLifeTimeModule.x = _lastWindForceFromTarget * 0.1f;
            }
        }
    }

    private void UpdateSnowForces()
    {
        if (_snowParticleSystem.isPlaying)
        {
            var forceOverLifeTimeModule = _snowParticleSystem.forceOverLifetime;

            forceOverLifeTimeModule.x = _lastWindForceFromTarget * 0.001f;
        }
    }

    public float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}