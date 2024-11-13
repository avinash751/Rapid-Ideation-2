using UnityEngine;
using System;

public class FishingMiniGameHandler : MonoBehaviour
{

    public static event Action OnFishingMiniGameStarted;
    public static event Action OnFishingSuccess;
    public static event Action OnFishingFailure;

    public static void StartMiniGame()
    {
        OnFishingMiniGameStarted?.Invoke();
        Debug.Log("Fishing mini-game started!");
    }

    public static void TriggerSuccess()
    {
        OnFishingSuccess?.Invoke();
        Debug.Log("Fishing mini-game success!");
    }

    public static void TriggerFailure()
    {
        OnFishingFailure?.Invoke();
        Debug.Log("Fishing mini-game failure!");
    }
}