using UnityEngine;
using UnityEngine.UI;

public class FishRewarder : MonoBehaviour
{
    [SerializeField] private Image finalFishImage;
    [SerializeField] private Sprite defaultFishIcon;

    private void OnEnable()
    {
        FishingMiniGameHandler.OnFishingSuccess += DisplayRewardedFish;
        FishingMiniGameHandler.OnFishingMiniGameStarted += ResetFishRewarder;
    }

    private void OnDisable()
    {
        FishingMiniGameHandler.OnFishingSuccess -= DisplayRewardedFish;
        FishingMiniGameHandler.OnFishingMiniGameStarted -= ResetFishRewarder;
    }

    void ResetFishRewarder(FishingSpot fishingSpot)
    {
        if (finalFishImage == null) return;
        finalFishImage.sprite = defaultFishIcon;

    }

    void DisplayRewardedFish(FishingSpot fishingSpot)
    {
        Fish rewardedFish = fishingSpot.GetFish();
        if (rewardedFish != null)
        {
            if (finalFishImage == null) return;
            finalFishImage.sprite = rewardedFish.FishSprite;

            // Increment the fish count in the mediator
            FishTrackerMediator.Instance.IncrementFishCount();

            fishingSpot.RemoveFish(rewardedFish);
        }
    }
}
