using System;
using UnityEngine;

public class WindController {

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

            var force = Math.Round(_WindTarget.transform.eulerAngles.y);

            if (force > 0 && _WindTarget.transform.rotation.y < 0) {
                force = 0;
            } else if (force > 150) {
                force = 150;
            }

            particleSystemForceOverTimeModule.x = (int)force / 10;

            //balanço dos galhos das árvores
            var material = tree.GetComponentInChildren<MeshRenderer>().materials[1];

            if (force > InitialTreeSwaySpeed && (force % 10 == 0) && force != 0) {
                material.SetFloat("_tree_sway_speed", (int)force / 10);
            }
        }
    }

    private void UpdateRainForces() {

    }
}