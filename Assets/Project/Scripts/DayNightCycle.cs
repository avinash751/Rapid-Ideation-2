using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Day-Night Cycle Settings")]
    [SerializeField] private float dayDuration = 120f; // Duration of a full day in seconds.
    [SerializeField] private Gradient lightColorGradient; // Color gradient for the light throughout the day.
    [SerializeField] private AnimationCurve lightIntensityCurve; // Controls light intensity over the day.

    [Header("Environment Settings")]
    [SerializeField] private Gradient skyColorGradient;
    [SerializeField] private Gradient equatorColorGradient;
    [SerializeField] private Gradient groundColorGradient;

    [Header("Fog Settings")]
    [SerializeField] private float minFog;
    [SerializeField] private float maxFog;
    [SerializeField] private Gradient fogColorGradient;
    [SerializeField] private AnimationCurve fogDensityCurve; // Fog density over the day.

    [Header("References")]
    [SerializeField] private Light directionalLight;

    [Range(0,1)][SerializeField]private float cycleProgress = 0f;

    void Update()
    {
        cycleProgress += Time.deltaTime / dayDuration;
        cycleProgress %= 1f; // Keeps cycleProgress within 0 to 1.

        UpdateDirectionalLight();
        UpdateAmbientLighting();
        UpdateFog();
    }

    private void UpdateDirectionalLight()
    {
        float lightAngle = cycleProgress * 360f - 20f; // Converts cycle progress to -90 to 270 degrees for rotation.
        directionalLight.transform.rotation = Quaternion.Euler(lightAngle,-170f, 0f);

        directionalLight.color = lightColorGradient.Evaluate(cycleProgress);
        directionalLight.intensity = lightIntensityCurve.Evaluate(cycleProgress);
    }

    private void UpdateAmbientLighting()
    {
        RenderSettings.ambientSkyColor = skyColorGradient.Evaluate(cycleProgress);
        RenderSettings.ambientEquatorColor = equatorColorGradient.Evaluate(cycleProgress);
        RenderSettings.ambientGroundColor = groundColorGradient.Evaluate(cycleProgress);
    }

    private void UpdateFog()
    {
        float fogDensityValue = fogDensityCurve.Evaluate(cycleProgress);
        RenderSettings.fogColor = fogColorGradient.Evaluate(cycleProgress);
        RenderSettings.fogDensity = Mathf.Lerp(minFog, maxFog, fogDensityValue);
    }
}

