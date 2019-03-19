using UnityEngine;

public class WindController : MonoBehaviour
{
    public void UpdateForces()
    {
        UpdateTreeForces();
    }

    private void UpdateTreeForces() {

        var trees = GameObject.FindGameObjectsWithTag("Tree");

        foreach (var tree in trees) {
            // Adicionar controle com o marcador

            //módulo de forceOverTime das animações de folhas caindo
            var particleSystemForceOverTimeModule = tree.GetComponentInChildren<ParticleSystem>().forceOverLifetime;

            particleSystemForceOverTimeModule.x = 10;
            //particleSystemForceOverTimeModule.z = 1;

            //balanço dos galhos das árvores
            var material = tree.GetComponentInChildren<MeshRenderer>().materials[1];

            material.SetFloat("_tree_sway_speed", 2f);
        }
    }
}