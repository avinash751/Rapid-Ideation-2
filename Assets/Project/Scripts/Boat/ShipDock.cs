using UnityEngine;
using System;
using System.Collections;

public class ShipDock : MonoBehaviour, IInteractable
{
    [field:SerializeField]public bool canInteract { get; set; } = true;

    [SerializeField] private Transform parkingSpot;
    [SerializeField] private float dockingDuration = 2f;
    [SerializeField] private LayerMask objectsAllowedToPark;
    public static event Action OnObjectParkingFinished;

    private GameObject objectToDock;

    public void Interact()
    {
        if (!canInteract) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5, objectsAllowedToPark);
        foreach (var collider in colliders)
        {
            objectToDock = collider.gameObject;
            objectToDock.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(ParkObject(objectToDock));
            break;
        }
    }

    private IEnumerator ParkObject(GameObject targetObject)
    {
        canInteract = false; // Prevent further interactions during docking


        Vector3 startPosition = targetObject.transform.position;
        Quaternion startRotation = targetObject.transform.rotation;

        Vector3 targetPosition = parkingSpot.position;
        Quaternion targetRotation = parkingSpot.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < dockingDuration)
        {
            elapsedTime += Time.deltaTime;

            targetObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dockingDuration);
            targetObject.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / dockingDuration);

            yield return null;
        }

        targetObject.transform.position = targetPosition;
        targetObject.transform.rotation = targetRotation;

        OnObjectParkingFinished?.Invoke();
        objectToDock = null; // Clear reference
    }

    private void OnDrawGizmos()
    {
        if (parkingSpot != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(parkingSpot.position, Vector3.one * 0.5f);
        }
    }
}
