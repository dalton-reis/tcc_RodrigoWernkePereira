using TMPro;
using UnityEngine;

public class TemperatureTarget : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI panelText;

    private TextMeshPro _temperatureText;

    void Start() {
        _temperatureText = GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        var transform = GetComponentInChildren<Transform>();

        var angle = transform.eulerAngles.y;

        if (angle > 0 && transform.rotation.y < 0) {
            angle = 0;
        }

        _temperatureText.text = Mathf.Round(angle) + " °C";
        panelText.text = Mathf.Round(angle) + " °C";
    }
}