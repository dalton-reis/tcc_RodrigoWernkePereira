using System;
using System.Collections;
using UnityEngine;

public class CloudController
{
    public bool ActiveClouds { get; set; }
    public bool IsOnRainingPosition { get; set; }
    public bool IsOnDefaultPosition { get; set; }

    private GameObject _clouds;
    private Func<IEnumerator, Coroutine> _startCoroutine;
    private float _movementSpeed;
    private Vector3 _cloudRainingPosition;
    private Vector3 _cloudsDefaultPosition;
    private bool _movingClouds;
    private WaitForEndOfFrame _waitForEndOfFrame;

    public CloudController(Func<IEnumerator, Coroutine> StartCoroutine)
    {
        _startCoroutine = StartCoroutine;
        _waitForEndOfFrame = new WaitForEndOfFrame();

        _movementSpeed = 0.8f;

        _clouds = GameObject.FindGameObjectWithTag("Clouds");

        _cloudRainingPosition = Vector3.zero;
        _cloudsDefaultPosition = _clouds.transform.localPosition;

        ActiveClouds = false;
        IsOnDefaultPosition = true;
        IsOnRainingPosition = false;
    }

    public void Update(float windForce, float temperature)
    {
        ActivateClouds(temperature);
        UpdateCloudsPosition(windForce);
    }

    void MoveToRainingPosition(float windForce)
    {
        if (windForce > 20f && !IsOnRainingPosition)
        {
            _startCoroutine(MoveToRainingPosition());
        }
    }

    void MoveToDefaultPosition(float windForce)
    {
        if (windForce < 20f && (ActiveClouds && IsOnRainingPosition && !_movingClouds))
        {
            _startCoroutine(MoveToDefaultPosition());
        }
    }

    void UpdateCloudsPosition(float windForce)
    {
        if (ActiveClouds && !_movingClouds)
        {
            MoveToRainingPosition(windForce);
            MoveToDefaultPosition(windForce);
        }
    }

    IEnumerator MoveToDefaultPosition()
    {
        while (!IsOnDefaultPosition)
        {
            _movingClouds = true;

            float step = _movementSpeed * Time.deltaTime;

            _clouds.transform.localPosition =
                Vector3.MoveTowards(_clouds.transform.localPosition, _cloudsDefaultPosition, step);

            if (_clouds.transform.localPosition.Equals(_cloudsDefaultPosition))
            {
                _movingClouds = false;
                IsOnDefaultPosition = true;
                IsOnRainingPosition = false;
            }

            yield return _waitForEndOfFrame;
        }
    }

    IEnumerator MoveToRainingPosition()
    {
        while (!IsOnRainingPosition)
        {
            _movingClouds = true;

            float step = _movementSpeed * Time.deltaTime;

            _clouds.transform.localPosition =
                Vector3.MoveTowards(_clouds.transform.localPosition, _cloudRainingPosition, step);

            if (_clouds.transform.localPosition.Equals(_cloudRainingPosition))
            {
                _movingClouds = false;
                IsOnRainingPosition = true;
                IsOnDefaultPosition = false;
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