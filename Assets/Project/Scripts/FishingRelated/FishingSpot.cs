using UnityEngine;
using System;

public class FishingSpot : MonoBehaviour, IInteractable
{
    [field: SerializeField] public bool canInteract { get; set; } = true;

    public void Interact()
    {
        if (canInteract)
        {
           FishingMiniGameHandler.StartMiniGame();
        }
    }
}
