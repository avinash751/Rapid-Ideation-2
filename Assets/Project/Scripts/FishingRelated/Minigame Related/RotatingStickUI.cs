using UnityEngine;
using System;

public class RotatingStickUI : MonoBehaviour
{
    public event Action OnTargetHit; 
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Transform pivot; 

    private void Update()
    {
        transform.RotateAround(pivot.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            OnTargetHit?.Invoke();
            other.gameObject.SetActive(false);
        }
    }
}

