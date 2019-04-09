using System;
using TMPro;
using UnityEngine;

public class WindTarget : MonoBehaviour {

    [SerializeField]
    public TextMeshProUGUI panelText;

    private TextMeshPro _text;

    void Start() {
        _text = GetComponentInChildren<TextMeshPro>();
    }

    void Update() {
        var transform = GetComponentInChildren<Transform>();

        var angle = transform.eulerAngles.y;

        if (angle > 0 && transform.rotation.y < 0) {
            angle = 0;
        } else if (angle > 150) {
            angle = 150;
        }

        _text.text = Math.Round(angle).ToString() + " km/h";
        panelText.text = Math.Round(angle).ToString() + " km/h";
    }
}
