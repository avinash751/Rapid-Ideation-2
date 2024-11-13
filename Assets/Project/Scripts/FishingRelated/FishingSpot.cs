using UnityEngine;

public class FishingSpot : MonoBehaviour, IInteractable
{
    FishGenerator fishPond;
    [field: SerializeField] public bool canInteract { get; set; } = true;

    private void Start()
    {
        TryGetComponent(out fishPond);
    }

    public void Interact()
    {
        if (fishPond == null) return;
        if (canInteract && IsFishAvailable())
        {
            FishingMiniGameHandler.StartMiniGame(this);
        }
    }

    public bool IsFishAvailable()
    {
        return fishPond != null && fishPond.fishInstances.Count > 0;
    }

    public Fish GetFish()
    {
        return fishPond.fishInstances[fishPond.fishInstances.Count - 1];
    }

    public void RemoveFish(Fish fishToRemove)
    {
        fishPond.fishInstances.Remove(fishToRemove);
        Destroy(fishToRemove.gameObject);
    }
}
