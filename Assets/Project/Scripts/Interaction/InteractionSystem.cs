using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private float boxDistance = 2.0f;
    [SerializeField] private Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactionKey;

    public Action<string> OnInteractableFound;
    public Action OnObjectUnInteractable;

    [SerializeField] private InputActionReference interactInput;

    [SerializeField] IInteractable foundInteractable;
    bool interactableFound;
    private void OnEnable()
    {
        interactInput.action.started += InteractWithObject;
    }

    private void OnDisable()
    {
        interactInput.action.started -= InteractWithObject;
    }
    void Update()
    {
        if (interactInput == null) return;
        CheckForInteractable();
    }

    void InteractWithObject(InputAction.CallbackContext obj)
    {

        if (!interactableFound || !foundInteractable.canInteract)
        {
            return;
        }

        if (foundInteractable.canInteract)
        {
            Debug.Log("Input was pressed");
            foundInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        Vector3 boxCenter = transform.position + transform.forward * boxDistance;

        Collider[] hits = Physics.OverlapBox(boxCenter, boxSize / 2, transform.rotation, interactableLayer, QueryTriggerInteraction.Collide);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out foundInteractable))
            {
                //Debug.Log(hit.gameObject.name);
                interactableFound = true;
            }
        }

        if (!interactableFound || foundInteractable == null || !foundInteractable.canInteract)
        {
            OnObjectUnInteractable?.Invoke();
            return;
        }
        else if (foundInteractable.canInteract)
        {
            OnInteractableFound.Invoke(interactionKey.ToString());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 boxCenter = transform.position + transform.forward * boxDistance;
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, boxSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}