using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowthStateManager
{
    private Func<IEnumerator, Coroutine> _startCoroutine;
    private GameObject[] _trees;
    private List<GameObject> _activeTrees;
    private List<GameObject> _disabledTrees;

    private WaitForSeconds _waitForSeconds;
    private SceneState _sceneState;
    private System.Random _random;

    public TreeGrowthStateManager(Func<IEnumerator, Coroutine> StartCoroutine)
    {
        _startCoroutine = StartCoroutine;
        _trees = GameObject.FindGameObjectsWithTag("Tree");

        _activeTrees = new List<GameObject>();
        _disabledTrees = new List<GameObject>();

        _waitForSeconds = new WaitForSeconds(5f);

        _random = new System.Random();

        RandomizeTrees();
        _startCoroutine.Invoke(GrowTrees());

    }

    public void Update(SceneState state)
    {
        _sceneState = state;
    }

    public IEnumerator GrowTrees()
    {
        while (true)
        {
            yield return _waitForSeconds;

            if (_sceneState == SceneState.Favorable)
            {
                foreach (var tree in _activeTrees)
                {
                    tree.GetComponent<Tree>().UpdateGrowthState();
                }

                if (_disabledTrees.Count > 0)
                {
                    int randomIndex = _random.Next(0, _disabledTrees.Count);

                    _disabledTrees[randomIndex].SetActive(true);
                    _activeTrees.Add(_disabledTrees[randomIndex]);
                    _disabledTrees.RemoveAt(randomIndex);
                }
            }
            else if (_sceneState == SceneState.Unfavorable)
            {
                if (_activeTrees.Count > 0)
                {
                    int randomIndex = _random.Next(0, _activeTrees.Count);

                    _activeTrees[randomIndex].SetActive(false);
                    _disabledTrees.Add(_activeTrees[randomIndex]);
                    _activeTrees.RemoveAt(randomIndex);
                }
            }
        }
    }

    void RandomizeTrees()
    {
        List<int> randomIndices = new List<int>();

        System.Random random = new System.Random();

        while (randomIndices.Count < 6)
        {
            int randomIndex = random.Next(_trees.Length);

            if (!randomIndices.Contains(randomIndex))
            {
                randomIndices.Add(randomIndex);
            }
        }
             
        foreach (var index in randomIndices)
        {
            var tree = _trees[index];

            tree.SetActive(false);

            _disabledTrees.Add(tree);
        }
               
        foreach (var tree in _trees)
        {
            if (tree.activeSelf)
            {
                _activeTrees.Add(tree);
            }
        }

        foreach (var tree in _activeTrees)
        {
            var growthState = (TreeGrowthState)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(TreeGrowthState)).Length);

            tree.gameObject.GetComponent<Tree>().TreeGrowthState = growthState;
        }
    }
}