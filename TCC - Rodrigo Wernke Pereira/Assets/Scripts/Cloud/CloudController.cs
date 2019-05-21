using UnityEngine;

public class CloudController
{
    private GameObject _clouds;

    private float movementSpeed = 0.8f * Time.deltaTime;

    private Vector3 _cloudFinalPosition;

    public bool CorrectPosition { get; set; }

    public bool ActiveClouds { get; set; }

    public CloudController()
    {
        _clouds = GameObject.FindGameObjectWithTag("Clouds");

        _cloudFinalPosition = Vector3.zero;

        ActiveClouds = false;
    }

    public void Update(float windForce, float temperature)
    {
        if (temperature >= 20 && !_clouds.activeSelf)
        {
            _clouds.SetActive(true);
            ActiveClouds = true;
        }

        if (windForce > 20f && ActiveClouds)
        {
            _clouds.transform.localPosition = Vector3.MoveTowards(_clouds.transform.localPosition, _cloudFinalPosition, movementSpeed);
        }

        CorrectPosition = _clouds.transform.localPosition.Equals(_cloudFinalPosition) ? true : false;
    }
}