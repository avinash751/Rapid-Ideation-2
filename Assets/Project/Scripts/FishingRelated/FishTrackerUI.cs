using UnityEngine;
using TMPro;

public class FishTrackerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishCountText;
    [SerializeField] string extraText;


    private void OnEnable()
    {
        FishTrackerMediator.Instance.OnFishAdded += UpdateFishCount;
        FishTrackerMediator.Instance.OnFishRemoved += UpdateFishCount;
    }

    private void OnDisable()
    {
        FishTrackerMediator.Instance.OnFishAdded -= UpdateFishCount;
        FishTrackerMediator.Instance.OnFishRemoved -= UpdateFishCount;
    }

    private void Start()
    {
        TryGetComponent(out fishCountText);
        if (fishCountText != null)
        {
            fishCountText.text = extraText + FishTrackerMediator.Instance.GetTotalFishCollected();
        }
         
    }

    private void UpdateFishCount(int totalFish)
    {
        if (fishCountText != null)
        {
            fishCountText.text = extraText + totalFish;
        }
    }
}
