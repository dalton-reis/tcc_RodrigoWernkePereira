using System;
using TMPro;
using UnityEngine;

public class WindTarget : MonoBehaviour {

    [SerializeField]
    public TextMeshProUGUI panelText;

    private TextMeshPro _text;

    private float _windVelocity;

    void Start() {
        _windVelocity = 0f;

        _text = GetComponentInChildren<TextMeshPro>();
    }

    void Update() {
        bool isBeingTracked = VuforiaTools.IsBeingTracked("Wind Target");

        var transform = GetComponentInChildren<Transform>();

        if (transform.localRotation.eulerAngles.y <= 280 && transform.localRotation.eulerAngles.y >= 0 && isBeingTracked) {
            _windVelocity = transform.localRotation.eulerAngles.y;
        }

        _text.text = Math.Round(_windVelocity).ToString() + " km/h";
        panelText.text = Math.Round(_windVelocity).ToString() + " km/h";
    }
}