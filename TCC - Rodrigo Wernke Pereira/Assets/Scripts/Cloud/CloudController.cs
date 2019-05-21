using System;
using System.Collections;
using UnityEngine;

public class CloudController
{
    public bool ActiveClouds { get; set; }
    public bool CorrectPosition { get; set; }

    private GameObject _clouds;
    private Func<IEnumerator, Coroutine> _startCoroutine;
    private float _movementSpeed;
    private Vector3 _cloudFinalPosition;
    private bool _movingClouds;
    private WaitForEndOfFrame _waitForEndOfFrame;

    public CloudController(Func<IEnumerator, Coroutine> StartCoroutine)
    {
        _startCoroutine = StartCoroutine;
        _waitForEndOfFrame = new WaitForEndOfFrame();

        _movementSpeed = 0.8f;

        _clouds = GameObject.FindGameObjectWithTag("Clouds");

        _cloudFinalPosition = Vector3.zero;

        ActiveClouds = false;
        CorrectPosition = false;
    }

    public void Update(float windForce, float temperature)
    {
        ActivateClouds(temperature);
        MoveToCorrectPosition(windForce);
    }

    void MoveToCorrectPosition(float windForce)
    {
        if (windForce > 20f && ActiveClouds && !CorrectPosition && !_movingClouds)
        {
            _startCoroutine(MoveToCorrectPosition());
        }
    }

    IEnumerator MoveToCorrectPosition()
    {
        while (!CorrectPosition)
        {
            _movingClouds = true;

            float step = _movementSpeed * Time.deltaTime;

            _clouds.transform.localPosition =
                Vector3.MoveTowards(_clouds.transform.localPosition, _cloudFinalPosition, step);

            if (_clouds.transform.localPosition.Equals(_cloudFinalPosition))
            {
                _movingClouds = false;
                CorrectPosition = true;
            }

            yield return _waitForEndOfFrame;
        }
    }

    public void ActivateClouds(float temperature)
    {
        if (temperature >= 20 && !_clouds.activeSelf)
        {
            _clouds.SetActive(true);
            ActiveClouds = true;
        }
    }
}