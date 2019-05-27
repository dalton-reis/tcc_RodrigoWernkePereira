using System.Collections;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]
    public TreeGrowthState TreeGrowthState;

    private Transform _treeTransform;

    private MeshRenderer _meshRenderer;

    private ParticleSystem _leavesParticleSystem;

    private Material _dryLeafMaterial;
    private Material _windLeafMaterial;

    private Color _dryLeafColor;
    private Color _windLeafColor;

    void Start()
    {
        _treeTransform = GetComponent<Transform>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();

        _leavesParticleSystem = GetComponentInChildren<ParticleSystem>();

        _dryLeafMaterial = Resources.Load("Materials/Tree/DryLeaf", typeof(Material)) as Material;
        _windLeafMaterial = Resources.Load("Materials/Tree/WindLeaves", typeof(Material)) as Material;

        _dryLeafColor = new Color((185 / 255f), (122 / 255f), (87 / 255f));
        _windLeafColor = _leavesParticleSystem.main.startColor.color;

        SetGrowthState();
    }

    public void SetGrowthState()
    {
        switch (TreeGrowthState)
        {
            case TreeGrowthState.Seed:
                {
                    #region Scaling 

                    var destinationScale = new Vector3(0.1f, 0.1f, 0.1f);
                    StartCoroutine(ScaleOverTime(1f, destinationScale));

                    #endregion

                    #region Update Particle Leaves
                    if (_leavesParticleSystem.isPlaying)
                    {
                        _leavesParticleSystem.Stop();
                    }
                    #endregion

                    #region Update Material
                    var materials = _meshRenderer.materials;

                    if (materials[1].name.Equals("DryLeaf (Instance)"))
                    {
                        materials[1] = _windLeafMaterial;

                        _meshRenderer.materials = materials;
                    }
                    #endregion

                    break;
                }
            case TreeGrowthState.Sprout:
                {
                    #region Scaling 
                    var destinationScale = new Vector3(0.4f, 0.4f, 0.4f);

                    StartCoroutine(ScaleOverTime(1f, destinationScale));
                    #endregion

                    #region Update Particle Leaves
                    if (_leavesParticleSystem.isPlaying)
                    {
                        _leavesParticleSystem.Stop();
                    }
                    #endregion

                    break;
                }
            case TreeGrowthState.Sapling:
                {
                    #region Scaling 
                    var destinationScale = new Vector3(0.6f, 0.6f, 0.6f);
                    StartCoroutine(ScaleOverTime(1f, destinationScale));
                    #endregion

                    #region Update Particle Leaves
                    var leavesParticleSystem = GetComponentInChildren<ParticleSystem>();

                    if (leavesParticleSystem.isPlaying)
                    {
                        leavesParticleSystem.Stop();
                    }
                    #endregion

                    break;
                }
            case TreeGrowthState.Mature:
                {
                    #region Scaling 
                    var destinationScale = new Vector3(1f, 1f, 1f);

                    StartCoroutine(ScaleOverTime(1f, destinationScale));
                    #endregion

                    #region Update Particle Leaves

                    var mainModule = _leavesParticleSystem.main;
                    mainModule.startColor = _windLeafColor;

                    if (!_leavesParticleSystem.isPlaying)
                    {
                        _leavesParticleSystem.Play();
                    }

                    #endregion

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
                    if (!_leavesParticleSystem.isPlaying)
                    {
                        _leavesParticleSystem.Play();
                    }

                    var mainModule = _leavesParticleSystem.main;

                    mainModule.startColor = _dryLeafColor;
                    #endregion

                    #region Scaling 
                    var destinationScale = new Vector3(1.1f, 1.1f, 1.1f);
                    StartCoroutine(ScaleOverTime(1f, destinationScale));
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

    IEnumerator ScaleOverTime(float time, Vector3 destinationScale)
    {
        Vector3 originalScale = _treeTransform.localScale;

        float currentTime = 0.0f;

        do
        {
            _treeTransform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
    }
}