using UnityEngine;
using UnityEngine.InputSystem;

public class FishingMiniGameController : MonoBehaviour
{
    [SerializeField] private int targetsRequired = 3;
    [SerializeField] private RotatingStickUI rotatingStick;
    [SerializeField] private TargetSpawnerUI targetSpawner;
    [SerializeField] private InputActionReference reFishInput;

    private FishingSpot selectedFishingSpot;
    private int targetsCaught = 0;
    private bool canReFish = false;

    private void OnEnable()
    {
        rotatingStick.onTargetHit += HandleTargetHit;
        FishingMiniGameHandler.OnFishingMiniGameStarted += InitializeMiniGame;
        reFishInput.action.performed += OnReFishInput;
    }

    private void OnDisable()
    {
        rotatingStick.onTargetHit -= HandleTargetHit;
        FishingMiniGameHandler.OnFishingMiniGameStarted -= InitializeMiniGame;
        reFishInput.action.performed -= OnReFishInput;
    }

    private void HandleTargetHit()
    {
        targetsCaught++;
        if (targetsCaught >= targetsRequired)
        {
            FishingMiniGameHandler.TriggerSuccess(selectedFishingSpot);
            EndMiniGame();
        }
    }

    private void EndMiniGame()
    {
        rotatingStick.enabled = false;
        targetSpawner.ClearTargets();
        canReFish = true;

        if (targetsCaught < targetsRequired)
        {
            FishingMiniGameHandler.TriggerFailure();
        }
    }

    private void OnReFishInput(InputAction.CallbackContext context)
    {
        if (canReFish && selectedFishingSpot.IsFishAvailable())
        {
            FishingMiniGameHandler.StartMiniGame(selectedFishingSpot);
        }
        else
        {
            Debug.Log("No fish available to catch.");
        }
    }

    private void InitializeMiniGame(FishingSpot fishingSpot)
    {
        if (fishingSpot != null)
        {
            selectedFishingSpot = fishingSpot;
        }
        targetSpawner.ClearTargets();
        rotatingStick.enabled = true;
        targetSpawner.SpawnTargets();
        int spawnedTargetCount = targetSpawner.targets.Length;
        targetsRequired = Random.Range(1, spawnedTargetCount + 1);

        canReFish = false;
        targetsCaught = 0;
    }

}
