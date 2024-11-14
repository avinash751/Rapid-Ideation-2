using UnityEngine;
using System;
using UnityEngine.InputSystem;

using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class RotatingStickUI : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float speedMultiplier = 1.5f;  // Factor to increase speed when holding input
    [SerializeField] private Transform pivot;
    [SerializeField] private InputActionReference catchInput;

    public Action onTargetHit;

    private float defaultSpeed;
    private bool isInTarget;
    private IBreakable foundTarget;

    private void Awake()
    {
        // Store the initial rotation speed as the default speed
        defaultSpeed = rotationSpeed;
    }

    private void Update()
    {
        // Adjust speed based on input hold
        AdjustSpeedOnInput();

        // Rotate the stick around the pivot point
        transform.RotateAround(pivot.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        // Check if input was released to catch target
        CatchTargetOnInput();
    }

    private void AdjustSpeedOnInput()
    {
        if (catchInput.action.IsPressed())
        {
            rotationSpeed = defaultSpeed * speedMultiplier;
        }
        else
        {
            rotationSpeed = defaultSpeed;
        }
    }

    private void CatchTargetOnInput()
    {
        if (catchInput.action.WasReleasedThisFrame() && isInTarget)
        {
            // Break the target and invoke the onTargetHit event
            foundTarget?.BreakObject();
            onTargetHit?.Invoke();

            if (UnityEngine.Random.value < 0.6f)
            {
                defaultSpeed = -defaultSpeed;  // Reverse the default speed
                rotationSpeed = defaultSpeed;  // Apply reversed speed immediately
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IBreakable target))
        {
            isInTarget = true;
            foundTarget = target;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IBreakable target))
        {
            isInTarget = false;
            foundTarget = target;
        }
    }
}