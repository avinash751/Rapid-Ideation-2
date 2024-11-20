using UnityEngine;
using System.Collections.Generic;

public class OceanManager : MonoBehaviour
{
    private static OceanManager _instance;
    public static OceanManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<OceanManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("Ocean Manager");
                    _instance = obj.AddComponent<OceanManager>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] private List<Transform> oceanObjectsTransforms; // List of ocean object transforms
    [SerializeField] private float waveAmplitude;
    [SerializeField] private float waveFrequency;
    [SerializeField] private float waveSpeed;
    [SerializeField] private float setHeight;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Ensure there's only one instance
        }
    }

    public float GetWaterHeightAtPosition(Vector3 position)
    {
        Transform closestOceanObject = GetClosestOceanObject(position);

        if (closestOceanObject == null)
        {
            Debug.LogError("No ocean object found.");
            return 0f;
        }

        return CalculateWaveHeight(closestOceanObject, position);
    }

    private Transform GetClosestOceanObject(Vector3 position)
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (var oceanObject in oceanObjectsTransforms)
        {
            float distance = Vector3.Distance(position, oceanObject.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = oceanObject;
            }
        }

        return closest;
    }

    private float CalculateWaveHeight(Transform closestOceanObject, Vector3 position)
    {
        if (closestOceanObject == null)
        {
            Debug.LogError("Ocean object not found!");
            return 0f;
        }

        // Get the displacement texture from the ocean object's material
        Material oceanMaterial = closestOceanObject.GetComponent<Renderer>().material;
        Texture2D displacementTexture = oceanMaterial.GetTexture("_DisplacementTexture") as Texture2D;

        if (displacementTexture == null)
        {
            Debug.LogError("Displacement texture not assigned for ocean object!");
            return 0f;
        }

        // Apply the formula to get wave height
        float displacementValue = displacementTexture.GetPixelBilinear(position.x * waveFrequency, position.z * waveFrequency * Time.time * waveSpeed).g;

        // Calculate the height based on the given formula
        return closestOceanObject.position.y + displacementValue * waveAmplitude * closestOceanObject.localScale.x + setHeight;
    }
}


