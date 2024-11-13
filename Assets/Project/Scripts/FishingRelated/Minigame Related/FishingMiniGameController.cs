using UnityEngine;

public class FishingMiniGameController : MonoBehaviour
{
    [SerializeField] private int targetsRequired = 3; // Number of targets to "catch" for success
    [SerializeField] private RotatingStickUI rotatingStick;
    [SerializeField] private TargetSpawnerUI targetSpawner;

    private int targetsCaught = 0;

    private void Start()
    {
        targetSpawner.SpawnTargets();
        rotatingStick.OnTargetHit += HandleTargetHit;
    }

    private void HandleTargetHit()
    {
        targetsCaught++;
        if (targetsCaught >= targetsRequired)
        {
            FishingMiniGameHandler.TriggerSuccess();
            EndMiniGame();
        }
    }

    private void EndMiniGame()
    {
        rotatingStick.enabled = false;
        targetSpawner.ClearTargets();

        // Optionally, trigger failure if not enough targets were caught
        if (targetsCaught < targetsRequired)
        {
            FishingMiniGameHandler.TriggerFailure();
        }
    }
}

