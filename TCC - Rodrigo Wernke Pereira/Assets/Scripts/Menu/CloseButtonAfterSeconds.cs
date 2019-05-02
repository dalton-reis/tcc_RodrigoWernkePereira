using System.Collections;
using UnityEngine;

public class CloseButtonAfterSeconds : MonoBehaviour
{
    void OnEnable()
    {
        CloseAfterSeconds();
    }

    void CloseAfterSeconds()
    {
        StartCoroutine(CloseButtonAfterFiveSeconds());
    }

    IEnumerator CloseButtonAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);

        gameObject.SetActive(false);
    }
}