using UnityEngine;
using UnityEngine.UI;

public class FishRewarder : MonoBehaviour
{
    [SerializeField] Image finalFishImage;
    [SerializeField] Sprite defaultFishIcon;

    private void OnEnable()
    {
        FishingMiniGameHandler.OnFishingSuccess += DisplayRewardedFish;
        FishingMiniGameHandler.OnFishingMiniGameStarted += ResetFishRewarder;
    }

    private void OnDisable()
    {
        FishingMiniGameHandler.OnFishingSuccess -= DisplayRewardedFish;
        FishingMiniGameHandler.OnFishingMiniGameStarted += ResetFishRewarder;
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
            fishingSpot.RemoveFish(rewardedFish);
        }
    }

}