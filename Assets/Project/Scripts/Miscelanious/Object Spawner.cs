using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject objectPrefab;        
    [SerializeField] private int amountToSpawn = 10;         
    [SerializeField] private Vector3 spawnAreaBounds = new Vector3(10, 0, 10); 
    [SerializeField] private Vector3 spawnOffset = Vector3.zero; 
    [SerializeField] private LayerMask collisionMask;        
    [SerializeField] private Vector3 overlapBoxSize = Vector3.one; 
    [SerializeField] private int maxAttemptsPerObject = 10;  

    private void Start()
    {
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 spawnPosition = FindValidSpawnPosition();
            if (spawnPosition != Vector3.zero)
            {
                Instantiate(objectPrefab, spawnPosition + spawnOffset, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"Failed to find a valid position for object {i + 1}/{amountToSpawn}");
            }
        }
    }

    private Vector3 FindValidSpawnPosition()
    {
        for (int attempt = 0; attempt < maxAttemptsPerObject; attempt++)
        {
            Vector3 randomPosition = GetRandomPositionWithinBounds();

            // Check for collisions using an overlap box cast
            Collider[] colliders = Physics.OverlapBox(randomPosition, overlapBoxSize / 2, Quaternion.identity, collisionMask,QueryTriggerInteraction.Collide);
            if (colliders.Length == 0)
            {
                return randomPosition;
            }
        }
        return Vector3.zero; // Return an invalid position if no valid spot is found
    }

    private Vector3 GetRandomPositionWithinBounds()
    {
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaBounds.x / 2, spawnAreaBounds.x / 2),
            Random.Range(-spawnAreaBounds.y / 2, spawnAreaBounds.y / 2),
            Random.Range(-spawnAreaBounds.z / 2, spawnAreaBounds.z / 2)
        );
        return randomPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaBounds);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, overlapBoxSize);
    }
}

