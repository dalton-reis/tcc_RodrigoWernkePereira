using System.Collections;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Vector3 _waterEvaporationPosition;
    private Vector3 _waterNormalPosition;

    private ParticleSystem _smokeParticleSystem;

    private void Start()
    {
        _waterNormalPosition = transform.localPosition;

        _waterEvaporationPosition = transform.localPosition;
        _waterEvaporationPosition.y -= 0.08f;

        _smokeParticleSystem = GetComponentInChildren<ParticleSystem>();
        _smokeParticleSystem.Stop();
    }

    public void Evaporate()
    {
        StartCoroutine(Evaporate(_waterNormalPosition, _waterEvaporationPosition,4f));
    }

    public void Condense()
    {
        StartCoroutine(Condense(_waterEvaporationPosition, _waterNormalPosition, 4f));
    }

    IEnumerator Evaporate(Vector3 currentPos, Vector3 targetPos, float timeToMove)
    {
        var t = 0f;

        _smokeParticleSystem.Play();

        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.localPosition = Vector3.Lerp(currentPos, targetPos, t);
            yield return null;
        }

        _smokeParticleSystem.Stop();
    }

    IEnumerator Condense(Vector3 currentPos, Vector3 targetPos, float timeToMove)
    {
        var t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.localPosition = Vector3.Lerp(currentPos, targetPos, t);
            yield return null;
        }
    }
}