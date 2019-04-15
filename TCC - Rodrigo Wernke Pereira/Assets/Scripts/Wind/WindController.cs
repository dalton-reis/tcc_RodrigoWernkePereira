using System;
using UnityEngine;

public class WindController {

    public double WindForce { get; set; }

    private GameObject _WindTarget;

    private Material _TreeBranches;

    private GameObject[] Trees;

    private float InitialTreeSwaySpeed;

    public WindController() {

        InitialTreeSwaySpeed = 3f;

        Trees = GameObject.FindGameObjectsWithTag("Tree");

        _WindTarget = GameObject.Find("Wind Target");

        foreach (var tree in Trees) {
            var material = tree.GetComponentInChildren<MeshRenderer>().materials[1];

            material.SetFloat("_tree_sway_speed", 3f);
        }
    }

    public void UpdateForces() {
        UpdateTreeForces();

        UpdateRainForces();
    }

    private void UpdateTreeForces() {

        foreach (var tree in Trees) {

            //módulo de forceOverTime das animações de folhas caindo
            var particleSystemForceOverTimeModule = tree.GetComponentInChildren<ParticleSystem>().forceOverLifetime;

            //adiciona força nas folhas
            WindForce = Math.Round(_WindTarget.transform.eulerAngles.y);

            if (WindForce > 0 && _WindTarget.transform.rotation.y < 0) {
                WindForce = 0;
            }

            particleSystemForceOverTimeModule.x = (int)WindForce / 10;

            //balanço dos galhos das árvores
            var material = tree.GetComponentInChildren<MeshRenderer>().materials[1];

            if (WindForce > InitialTreeSwaySpeed && (WindForce % 10 == 0) && WindForce != 0) {
                material.SetFloat("_tree_sway_speed", (int)WindForce / 10);
            }
        }
    }

    private void UpdateRainForces() {

    }
}