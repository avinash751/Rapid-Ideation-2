using UnityEngine;
using System.Collections.Generic;

public class FishGenerator : MonoBehaviour
{
    [Header("Fish Settings")]
    [SerializeField] private Fish fishPrefab;          
    [SerializeField] private int minFishCount = 1;           
    [SerializeField] private int maxFishCount = 5;           
    [field:SerializeField] public Vector3 spawnAreaSize {  get; set; }
     

    public List<Fish> fishInstances = new List<Fish>();


    private void Start()
    {
        GenerateFish();
    }

    private void GenerateFish()
    {
        int fishCount = Random.Range(minFishCount, maxFishCount + 1);
        for (int i = 0; i < fishCount; i++)
        {
            // Random position within the defined spawn area
            Vector3 randomPosition = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            Fish fish = Instantiate(fishPrefab, randomPosition, Quaternion.identity);
            fish.transform.SetParent(transform);
            fishInstances.Add(fish);
        }
    }

}   