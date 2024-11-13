using UnityEngine;

public class FishMovement: MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private Vector3 swimRange = new Vector3(3, 3, 3);

    private Vector3 targetPosition;

    private void Start()
    {
        SetNewTargetPosition();
    }

    private void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    private void SetNewTargetPosition()
    {
        targetPosition = transform.parent.position + new Vector3(
            Random.Range(-swimRange.x, swimRange.x),
            Random.Range(-swimRange.y, swimRange.y),
            Random.Range(-swimRange.z, swimRange.z)
        );
    }
}