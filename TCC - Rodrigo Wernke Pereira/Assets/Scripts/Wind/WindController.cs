using UnityEngine;

public class WindController
{
    private GameObject _windTarget;
    private Material _treeBranches;
    private GameObject[] _trees;
    private float _initialTreeSwaySpeed;
    private WindTextManager _windTextManager;

    public float WindForce { get; set; }

    public WindController()
    {
        _windTextManager = new WindTextManager();

        _initialTreeSwaySpeed = 3f;

        _windTarget = GameObject.Find("Wind Target");

        SetInitialTreeSwaySpeed();
    }

    public void Update()
    {
        UpdateWindForce();

        _windTextManager.Update(WindForce);

        UpdateTreeForces();

        UpdateRainForces();
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

    private void UpdateWindForce()
    {
        bool isBeingTracked = VuforiaTools.IsBeingTracked("Wind Target");

        var windTargetTransform = _windTarget.GetComponentInChildren<Transform>();

        if (windTargetTransform.localRotation.eulerAngles.y <= 280 && windTargetTransform.localRotation.eulerAngles.y >= 0 && isBeingTracked)
        {
            WindForce = windTargetTransform.localRotation.eulerAngles.y;
        }
    }

    private void UpdateTreeForces()
    {
        foreach (var tree in _trees)
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

    private void UpdateRainForces()
    {
        // TO DO
    }
}