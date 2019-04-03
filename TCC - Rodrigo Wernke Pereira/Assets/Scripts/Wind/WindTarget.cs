using System;
using UnityEngine;

public class WindTarget : MonoBehaviour {
    private TextMesh _textMesh;

    void Start() {
        _textMesh = GetComponentInChildren<TextMesh>();
    }

    void Update() {
        var transform = GetComponentInChildren<Transform>();

        var angle = transform.eulerAngles.y;

        if (angle > 0 && transform.rotation.y < 0) {
            angle = 0;
        } else if (angle > 150) {
            angle = 150;
        }

        _textMesh.text = Math.Round(angle).ToString();
    }
}
