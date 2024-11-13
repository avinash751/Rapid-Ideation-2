using UnityEngine;

public class FishingMiniGameUI : MonoBehaviour
{
    [SerializeField] private GameObject fishingUI;

    private void OnEnable()
    {
        FishingMiniGameHandler.OnFishingMiniGameStarted += EnableFishingUI;
    }

    private void OnDisable()
    {
        FishingMiniGameHandler.OnFishingMiniGameStarted += EnableFishingUI;
    }

    private void EnableFishingUI()
    {
        fishingUI?.SetActive(true);
        Debug.Log("Fishing UI enabled.");
    }
}

