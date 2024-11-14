using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class RotatingStickUI : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Transform pivot;
    [SerializeField] InputActionReference catchInput;
    public Action onTargetHit;

    bool isInTarget;
    IBreakable foundTarget;

    private void Update()
    {
        transform.RotateAround(pivot.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        CatchTargetOnInput();

    }

    private void CatchTargetOnInput()
    {
        if (catchInput.action.triggered && isInTarget)
        {
            foundTarget?.BreakObject();
            onTargetHit?.Invoke();

            if (UnityEngine.Random.value < 0.6f)
            {
                rotationSpeed = rotationSpeed * -1;
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

