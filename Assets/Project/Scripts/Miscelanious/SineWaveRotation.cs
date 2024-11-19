using UnityEngine;

public class SineWaveRotation : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minRotationAngle;
    [SerializeField] private float maxRotationAngle;
    private float rotationRange = 45f;
    private float rotationSpeed = 30f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    private void Start()
    {
        rotationRange = Random.Range(minRotationAngle, maxRotationAngle);
        rotationSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * rotationRange;
        transform.rotation = Quaternion.Euler(rotationAxis * rotationAngle);
    }
}

