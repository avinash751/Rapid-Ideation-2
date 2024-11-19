using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    [Header("Acceleration Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float accelerationDuration = 2f;
    [SerializeField] private AnimationCurve accelerationCurve;

    [Header("Deceleration Settings")]
    [SerializeField] private float decelerationDuration = 2f;
    [SerializeField] private AnimationCurve decelerationCurve;

    [Header("Steering Settings")]
    [SerializeField] private float rotationTorque = 50f; 

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InputActionReference moveInput;

    private Vector3 targetVelocity;
    private Vector2 inputDirection;
    private float accelerationTime;
    private float decelerationTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (accelerationCurve == null || accelerationCurve.length == 0)
        {
            accelerationCurve = AnimationCurve.Linear(0, 0, 1, 1);
        }

        if (decelerationCurve == null || decelerationCurve.length == 0)
        {
            decelerationCurve = AnimationCurve.Linear(0, 1, 1, 0);
        }
    }

    private void Update()
    {
        if (moveInput != null)
        {
            inputDirection = moveInput.action.ReadValue<Vector2>();

            if (inputDirection == Vector2.zero)
            {
                accelerationTime = 0f;        
            }
            else
            {
                decelerationTime = 0f;      
                targetVelocity = transform.forward * (inputDirection.y * moveSpeed);
            }        
        }
    }

    private void FixedUpdate()
    {
        if (targetVelocity != Vector3.zero)
        {
            GetAccelerationForce(out Vector3 desiredAccelerationVelocity);
            rb.AddForce(desiredAccelerationVelocity, ForceMode.Force);
        }
        else
        {
            GetDecelerationForce(out Vector3 decelerationVelocity);
            rb.AddForce(decelerationVelocity, ForceMode.Force);
        }

        ApplyBoatTorque();
    }

    private void ApplyBoatTorque()
    {
        if (inputDirection.x != 0)
        {
            float targetRotationTorque = inputDirection.x * rotationTorque;
            rb.AddTorque(Vector3.up * targetRotationTorque * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    private void GetAccelerationForce(out Vector3 currentVelocity)
    {
        accelerationTime = Mathf.Min(accelerationTime + Time.deltaTime, accelerationDuration);
        float curveValue = accelerationCurve.Evaluate(accelerationTime / accelerationDuration);
        currentVelocity = Vector3.Lerp(Vector3.zero, targetVelocity, curveValue);
    }

    private void GetDecelerationForce(out Vector3 decelerationVelocity)
    {
        decelerationTime = Mathf.Min(decelerationTime + Time.deltaTime, decelerationDuration);
        float decelerationFactor = decelerationCurve.Evaluate(decelerationTime / decelerationDuration);
        decelerationVelocity = rb.linearVelocity * decelerationFactor;

        if (decelerationTime >= decelerationDuration)
        {
            decelerationVelocity = Vector3.zero;
        }
    }
}