using UnityEngine;

public class TargetSpawnerUI : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab; 
    [SerializeField] private int targetCount = 5;     
    [SerializeField] private float radius = 2.5f;     

    private GameObject[] targets;

    public void SpawnTargets()
    {
        targets = new GameObject[targetCount];

        for (int i = 0; i < targetCount; i++)
        {
            // Calculate the angle and position for each target around the circle
            float angle = i * Mathf.PI * 2 / targetCount;
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            targets[i] = Instantiate(targetPrefab, transform.position + position, Quaternion.identity, transform);
        }
    }

    public void ClearTargets()
    {
        foreach (var target in targets)
        {
            if (target != null) Destroy(target);
        }
    }
}
