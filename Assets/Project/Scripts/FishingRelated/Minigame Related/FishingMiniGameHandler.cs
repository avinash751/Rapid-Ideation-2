using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class FishingMiniGameHandler : MonoBehaviour
{
    public static event Action<FishingSpot> OnFishingMiniGameStarted;
    public static event Action<FishingSpot> OnFishingSuccess;
    public static event Action OnFishingFailure;
    public static event Action OnMiniGameExit;  // New event for mini-game exit

    [SerializeField] private InputActionReference exitMiniGameInput;
    static PlayerInput playerInput;

    private void Start()
    {
        if (playerInput == null)
        {
            playerInput = FindAnyObjectByType<PlayerInput>();
        }
    }

    private void Update()
    {
        if (exitMiniGameInput.action.triggered)
        {
            ExitMiniGame();
        }
    }

    private void OnDisable()
    {
        playerInput.currentActionMap = playerInput.actions.FindActionMap("Fishing Gameplay");
        playerInput.currentActionMap.Enable();
    }

    public static void StartMiniGame(FishingSpot triggeredFishingSpot)
    {
        OnFishingMiniGameStarted?.Invoke(triggeredFishingSpot);
        playerInput.currentActionMap = playerInput.actions.FindActionMap("FishingMIniGameUI");
        playerInput.currentActionMap.Enable();
        Debug.Log("Fishing mini-game started!");
    }

    public static void TriggerSuccess(FishingSpot triggeredFishingSpot)
    {
        OnFishingSuccess?.Invoke(triggeredFishingSpot);
        Debug.Log("Fishing mini-game success!");
    }

    public static void TriggerFailure()
    {
        OnFishingFailure?.Invoke();
        Debug.Log("Fishing mini-game failure!");
    }

    private void ExitMiniGame()
    {
        playerInput.currentActionMap = playerInput.actions.FindActionMap("Fishing Gameplay");
        playerInput.currentActionMap.Enable();

        OnMiniGameExit?.Invoke();
    }
}
