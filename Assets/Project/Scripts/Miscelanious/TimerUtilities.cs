using System.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine;

[System.Serializable]
public class TimerUtilities
{
    [SerializeField] private bool showDecimal;
    [SerializeField] private TextMeshProUGUI textReference;
    [SerializeField] private string addonText;
    [SerializeField] private bool updateTextOnTimer; 
    private Color textStartColor;

    // Public properties with private setters
    [field: SerializeField] public float ElapsedTime { get; private set; } 
    [field: SerializeField] public float TimeLeft { get; private set; }    
    [field: SerializeField] public float TargetStopTime { get; private set; } 
    [field: SerializeField] public bool IsRunning { get; private set; } 

    private CancellationTokenSource cts;
    public enum TimerType { UnlimitedStopWatch, NormalStopWatch, CountDown }
    private TimerType currentTimerType;

    // These methods needs to be used outside this class
    #region Public Functions

    public TimerUtilities(bool showDecimal = false,bool updateTextOnTimer = false,string addonText = "")
    {
        this.showDecimal = showDecimal;
        this.addonText = addonText;
        this.updateTextOnTimer = updateTextOnTimer;
    }

    /// <summary>
    /// Initializes an unlimited stopwatch that runs indefinitely.
    /// </summary>
    public void InitializeUnlimitedStopWatch()
    {
        ResetTimer();
        currentTimerType = TimerType.UnlimitedStopWatch;
        StartTimer();
    }

    /// <summary>
    /// Initializes a stopwatch that runs until a target time.
    /// </summary>
    public void InitializeNormalStopWatch(float targetTime)
    {
        ResetTimer();
        TargetStopTime = targetTime;
        currentTimerType = TimerType.NormalStopWatch;
        StartTimer();
    }

    /// <summary>
    /// Initializes a countdown timer.
    /// </summary>
    public void InitializeCountDown(float startTime)
    {
        ResetTimer();
        TimeLeft = startTime;
        currentTimerType = TimerType.CountDown;
        StartTimer();
    }

    /// <summary>
    /// Stops the current timer and optionally resets its elapsed time.
    /// </summary>
    public void StopTimer(bool resetElapsedTime = false)
    {
        if (cts != null)
        {
            cts.Cancel();
        }
        IsRunning = false;
        if (resetElapsedTime)
        {
            ResetTimer();
        }
    }


    /// <summary>
    /// Changes the color of the timer text for a specified duration.
    /// </summary>
    public async void ChangeTextColor(Color newColor, float duration)
    {
        if (textReference == null) return;
        Color originalColor = textReference.color;

        textReference.color = newColor;
        await Task.Delay((int)(duration * 1000));

        if (textReference != null)
        {
            textReference.color = originalColor;
        }
    }

    /// <summary>
    /// Enable or disable the text update behavior.
    /// </summary>
    public void ToggleTextUpdate(bool enable)
    {
        updateTextOnTimer = enable;
    }

    /// <summary>
    /// Updates the timer text with the current time value.
    /// </summary>
    public void UpdateText(float timeValue)
    {
        if (textReference != null)
        {
            string formattedTime = showDecimal ? timeValue.ToString("F2") : timeValue.ToString("F0");
            textReference.text = addonText + formattedTime;
        }
    }

    #endregion


    #region Backend Private Functions
    /// <summary>
    /// Starts the timer in the background using real-time updates.
    /// </summary>
    private async void StartTimer()
    {
        if (IsRunning) return;
        IsRunning = true;
        cts = new CancellationTokenSource();
        textStartColor = textReference != null ? textReference.color : Color.white;

        float lastFrameTime = Time.realtimeSinceStartup;

        try
        {
            while (!cts.Token.IsCancellationRequested)
            {
                float currentTime = Time.realtimeSinceStartup;
                float deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                UpdateTimerLogic(deltaTime);
                await Task.Delay(1, cts.Token); // Minimal delay to avoid blocking the main thread
            }
        }
        catch (TaskCanceledException)
        {
            // Timer was stopped
        }
        finally
        {
            IsRunning = false;
        }
    }

    /// <summary>
    /// Handles timer updates based on the current timer type.
    /// </summary>
    private void UpdateTimerLogic(float deltaTime)
    {
        switch (currentTimerType)
        {
            case TimerType.UnlimitedStopWatch:
                UpdateUnlimitedStopWatch(deltaTime);
                break;

            case TimerType.NormalStopWatch:
                UpdateNormalStopWatch(deltaTime);
                break;

            case TimerType.CountDown:
                UpdateCountDown(deltaTime);
                break;
        }
    }

    private void UpdateUnlimitedStopWatch(float deltaTime)
    {
        ElapsedTime += deltaTime;
        if (ShouldUpdateText()) UpdateText(ElapsedTime);
    }

    private void UpdateNormalStopWatch(float deltaTime)
    {
        if (ElapsedTime < TargetStopTime)
        {
            ElapsedTime += deltaTime;
            if (ShouldUpdateText()) UpdateText(ElapsedTime);
        }
        else
        {
            StopTimer();
        }
    }

    private void UpdateCountDown(float deltaTime)
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= deltaTime;
            if (ShouldUpdateText()) UpdateText(TimeLeft);
        }
        else
        {
            StopTimer();
        }
    }

    /// <summary>
    /// Determines whether the text should be updated based on the configuration.
    /// </summary>
    private bool ShouldUpdateText()
    {
        return textReference != null && updateTextOnTimer; // Check the flag to see if the text should be updated
    }



    /// <summary>
    /// Resets the timer state and cancels any ongoing tasks.
    /// </summary>
    private void ResetTimer()
    {
        ElapsedTime = 0;
        TimeLeft = TargetStopTime;
        IsRunning = false;

        if (cts != null)
        {
            cts.Cancel();
        }
    }
    #endregion 
}
