using System;
using TMPro;
using UnityEngine;

public class WindTextManager
{
    private TextMeshProUGUI _panelText;

    private TextMeshPro _targetText;

    private GameObject _windTarget;

    private GameObject _windPanelText;

    public WindTextManager()
    {
        _windTarget = GameObject.Find("Wind Target");

        _targetText = _windTarget.GetComponentInChildren<TextMeshPro>();

        _windPanelText = GameObject.Find("WindForcePanelText");

        _panelText = _windPanelText.GetComponent<TextMeshProUGUI>();
    }

    public void Update(float windForce)
    {
        _panelText.text = $"{Math.Round(windForce).ToString()} km/h";

        _targetText.text = $"{Math.Round(windForce).ToString()} km/h";
    }
}