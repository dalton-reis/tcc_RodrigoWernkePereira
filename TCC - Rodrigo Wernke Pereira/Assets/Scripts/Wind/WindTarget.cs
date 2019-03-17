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
        var foward = transform.forward;

        _textMesh.text = foward.x.ToString() + "x " + foward.y.ToString()
            + "y " + foward.z.ToString() + "z";
    }
}
