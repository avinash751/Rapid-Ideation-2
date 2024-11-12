using UnityEngine;
using System.Collections.Generic;

public class FishTrigger : MonoBehaviour,IInteractable
{
    [Header("Fish Settings")]
    [SerializeField] private GameObject fishPrefab;          
    [SerializeField] private int minFishCount = 1;           
    [SerializeField] private int maxFishCount = 5;           
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(5, 5, 5); 
     

    private List<GameObject> fishInstances = new List<GameObject>();

    [field: SerializeField] public bool canInteract { get; set; }

    private void Start()
    {
        canInteract = true;
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

            GameObject fish = Instantiate(fishPrefab, randomPosition, Quaternion.identity);
            fish.transform.SetParent(transform);
            fishInstances.Add(fish);
        }
    }


    public void Interact()
    {
       
    }
}   