using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]
    public int InitialLife;
    [SerializeField]
    public TreeGrowthState TreeGrowthState;

    private Transform _treeTransform;
    private MeshRenderer _meshRenderer;
    private Material _dryLeafMaterial;
    private Material _windLeafMaterial;

    void Start()
    {
        _treeTransform = GetComponent<Transform>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();

        SetGrowthState();

        _dryLeafMaterial = Resources.Load("Materials/Tree/DryLeaf", typeof(Material)) as Material;
        _windLeafMaterial = Resources.Load("Materials/Tree/WindLeaves", typeof(Material)) as Material;
    }

    public void SetGrowthState()
    {
        switch (TreeGrowthState)
        {
            case TreeGrowthState.Seed:
                {
                    _treeTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                    var leavesParticleSystem = GetComponentInChildren<ParticleSystem>();

                    if (leavesParticleSystem.isPlaying)
                    {
                        leavesParticleSystem.Stop();
                    }

                    var materials = _meshRenderer.materials;

                    if (materials[1].name.Equals("DryLeaf (Instance)"))
                    {
                        materials[1] = _windLeafMaterial;

                        _meshRenderer.materials = materials;
                    }

                    break;
                }
            case TreeGrowthState.Sprout:
                {
                    _treeTransform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

                    var leavesParticleSystem = GetComponentInChildren<ParticleSystem>();

                    if (leavesParticleSystem.isPlaying)
                    {
                        leavesParticleSystem.Stop();
                    }
                    break;
                }
            case TreeGrowthState.Sapling:
                {
                    _treeTransform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

                    var leavesParticleSystem = GetComponentInChildren<ParticleSystem>();

                    if (leavesParticleSystem.isPlaying)
                    {
                        leavesParticleSystem.Stop();
                    }
                    break;
                }
            case TreeGrowthState.Mature:
                {
                    _treeTransform.localScale = new Vector3(1f, 1f, 1f);

                    var leavesParticleSystem = GetComponentInChildren<ParticleSystem>();

                    if (!leavesParticleSystem.isPlaying)
                    {
                        leavesParticleSystem.Play();
                    }
                    break;
                }
            case TreeGrowthState.Snag:
                {
                    #region Update Material

                    var materials = _meshRenderer.materials;

                    materials[1] = _dryLeafMaterial;

                    _meshRenderer.materials = materials;
                    #endregion
                    #region Update Particle Leaves
                    var leavesParticleSystem = GetComponentInChildren<ParticleSystem>();

                    if (!leavesParticleSystem.isPlaying)
                    {
                        leavesParticleSystem.Play();
                    }

                    var mainModule = leavesParticleSystem.main;

                    var dryLeafColor = new Color((185 / 255f), (122 / 255f), (87 / 255f));

                    mainModule.startColor = dryLeafColor;
                    #endregion
                    break;
                }
            default:
                return;
        }

    }

    public void UpdateGrowthState()
    {
        Grow();
        SetGrowthState();
    }

    public void Grow()
    {
        switch (TreeGrowthState)
        {
            case TreeGrowthState.Seed:
                TreeGrowthState = TreeGrowthState.Sprout;
                return;
            case TreeGrowthState.Sprout:
                TreeGrowthState = TreeGrowthState.Sapling;
                return;
            case TreeGrowthState.Sapling:
                TreeGrowthState = TreeGrowthState.Mature;
                return;
            case TreeGrowthState.Mature:
                TreeGrowthState = TreeGrowthState.Snag;
                return;
            case TreeGrowthState.Snag:
                TreeGrowthState = TreeGrowthState.Seed;
                return;
            default:
                TreeGrowthState = TreeGrowthState.Seed;
                return;
        }
    }
}