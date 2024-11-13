using UnityEngine;

public class FishingMiniGameUI : MonoBehaviour
{
    [SerializeField] private GameObject fishingUI;

    private void OnEnable()
    {
        FishingMiniGameHandler.OnFishingMiniGameStarted += EnableFishingUI;
        FishingMiniGameHandler.OnMiniGameExit += DisableFishingUI; // Listen for exit event
    }

    private void OnDisable()
    {
        FishingMiniGameHandler.OnFishingMiniGameStarted -= EnableFishingUI;
        FishingMiniGameHandler.OnMiniGameExit -= DisableFishingUI;
    }

    private void EnableFishingUI(FishingSpot triggeredFishingSpot)
    {
        fishingUI?.SetActive(true);
    }

    private void DisableFishingUI()
    {
        fishingUI?.SetActive(false);
    }
}
