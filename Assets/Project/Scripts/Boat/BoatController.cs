using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{

    [Header("Control Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float accelerationDuration = 2f;
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private float moveDrag;

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InputActionReference moveInput;

    private Vector3 moveDirection;
    private Vector3 targetVelocity;
    private float accelerationTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Set a default linear curve if none is provided
        if (accelerationCurve == null || accelerationCurve.length == 0)
        {
            accelerationCurve = AnimationCurve.Linear(0, 0, 1, 1);
        }
    }

    private void Update()
    {
        if (moveInput != null)
        {
            Vector2 inputDirection = moveInput.action.ReadValue<Vector2>();
            moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;

            // Reset acceleration time on new input
            if (moveDirection == Vector3.zero)
            {
                accelerationTime = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        targetVelocity = moveDirection * moveSpeed;

        if (moveDirection != Vector3.zero)
        {

            float curveValue = 0;
            Vector3 currentVelocity = Vector3.zero;

            if (accelerationTime < accelerationDuration)
            {
                accelerationTime += Time.fixedDeltaTime;
                curveValue = accelerationCurve.Evaluate(accelerationTime / accelerationDuration);
                currentVelocity = Vector3.Lerp(Vector3.zero, targetVelocity, curveValue);
            }
            else
            {
                currentVelocity = targetVelocity;
            }

            rb.AddForce(currentVelocity, ForceMode.Force);

            if (accelerationTime >= accelerationDuration)
            {
                accelerationTime = accelerationDuration;
            }

            Vector3 rotationForce = new Vector3(moveDirection.y, 0, moveDirection.x);
            rb.AddTorque(rotationForce);
        }
        else
        {
            rb.linearVelocity *= moveDrag;
        }
    }
}


