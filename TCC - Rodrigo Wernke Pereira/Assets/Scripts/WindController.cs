using UnityEngine;

public class WindController : MonoBehaviour
{
    public void UpdateForces()
    {
        var trees = GameObject.FindGameObjectsWithTag("Tree");

        foreach (var tree in trees)
        {
            // Adicionar controle com o marcador
            
            //folhas caindo
            var particleSystemForceOverTimeModule = tree.GetComponentInChildren<ParticleSystem>().forceOverLifetime;
            
            particleSystemForceOverTimeModule.x = 1;
            particleSystemForceOverTimeModule.z = 1;

            //balanço dos galhos
            var material = tree.GetComponentInChildren<MeshRenderer>().materials[1];

            material.SetFloat("_tree_sway_speed", 1f);
        }
    }
}