using UnityEngine;

public class WindTarget : MonoBehaviour
{
    private TextMesh _textMesh;

    void Start()
    {
        _textMesh = GetComponentInChildren<TextMesh>();       
    }

    void Update()
    {
        var forward = transform.forward;

        _textMesh.text = forward.y.ToString() + "y";
    }
}
