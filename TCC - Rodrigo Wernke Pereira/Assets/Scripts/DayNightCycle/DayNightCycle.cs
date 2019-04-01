using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    private float _initialPosition;

    public GameObject GameObject;

    [SerializeField]
    public float InitialPosition;

    [SerializeField]
    public float Speed;

    [SerializeField]
    public float Distance;

    [SerializeField]
    public float Height;

    private void Start() {
        _initialPosition = InitialPosition;
    }

    private void Update() {
        _initialPosition += Time.deltaTime * Speed;

        float x = Mathf.Cos(_initialPosition) * Distance;
        float y = Mathf.Sin(_initialPosition) * Height;
        float z = 0;

        transform.position = new Vector3(x, y, z);
    }
}