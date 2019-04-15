using TMPro;
using UnityEngine;
using Vuforia;

public class TemperatureTarget : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI panelText;

    [SerializeField]
    public GameObject Clouds;

    private TextMeshPro _temperatureText;

    private float _temperature;

    void Start() {
        _temperature = 0f;

        _temperatureText = GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        bool isBeingTracked = VuforiaTools.IsBeingTracked("Temperature Target");

        var transform = GetComponentInChildren<Transform>();

        if (transform.localRotation.eulerAngles.y <= 280 && transform.localRotation.eulerAngles.y >= 0 && isBeingTracked) {
            _temperature = transform.localRotation.eulerAngles.y;
        }
        
        _temperatureText.text = Mathf.Round(_temperature) + " °C";

        panelText.text = Mathf.Round(_temperature) + " °C";
    }

    
}