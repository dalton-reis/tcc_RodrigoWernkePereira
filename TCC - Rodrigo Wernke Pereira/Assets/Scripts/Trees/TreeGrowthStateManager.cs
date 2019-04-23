using System.Collections;
using UnityEngine;

public class TreeGrowthStateManager
{
    private GameObject[] _trees;

    private WaitForSeconds _waitForSeconds;

    public TreeGrowthStateManager()
    {
        _trees = GameObject.FindGameObjectsWithTag("Tree");
        _waitForSeconds = new WaitForSeconds(3f);
    }

    public IEnumerator GrowTrees()
    {
        while (true)
        {
            yield return _waitForSeconds;

            foreach (var tree in _trees)
            {
                tree.GetComponent<Tree>().UpdateGrowthState();
            }
        }
    }
}