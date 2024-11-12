using TMPro;
using UnityEngine;

public class InteractionTextDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionTextMesh;
    private InteractionSystem playerInteraction; // Reference to PlayerInteraction
    [SerializeField] string interactionText;

    private void Start()
    {
        // Subscribe to interaction events
        playerInteraction = FindAnyObjectByType<InteractionSystem>();
        if (playerInteraction == null) return;
        playerInteraction.OnInteractableFound += ShowText;
        playerInteraction.OnObjectUnInteractable += HideText;
    }

    private void OnDisable()
    {
        // Unsubscribe from interaction events
        if (playerInteraction == null) return;
        playerInteraction.OnInteractableFound -= ShowText;
        playerInteraction.OnObjectUnInteractable -= HideText;
    }

    private void ShowText(string _interactionKey)
    {
        if (interactionText == null) return;
        interactionText = "Press " + _interactionKey;
        interactionTextMesh.text = interactionText;
        interactionTextMesh.gameObject.SetActive(true);
    }

    private void HideText()
    {
        interactionTextMesh.text = "";
    }
}