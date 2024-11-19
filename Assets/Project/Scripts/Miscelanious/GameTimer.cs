
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] TimerUtilities stopWatch = new TimerUtilities();

    private void OnEnable()
    {
        ShipDock.OnObjectParkingFinished += StopGameTimer;
    }

    private void OnDisable()
    {
        ShipDock.OnObjectParkingFinished -= StopGameTimer;
    }
    private void Start()
    {
        stopWatch.InitializeUnlimitedStopWatch();
    }

    void StopGameTimer()
    {
        stopWatch.UpdateText(stopWatch.ElapsedTime);
        stopWatch.StopTimer();
    }

}