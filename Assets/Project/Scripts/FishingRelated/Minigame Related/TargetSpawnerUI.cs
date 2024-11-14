using UnityEngine;

public class TargetSpawnerUI : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private int minTargetCount = 3;
    [SerializeField] private int maxTargetCount = 7;
    [SerializeField] private float radius = 2.5f;

    public GameObject[] targets {  get; private set; }

    public void SpawnTargets()
    {
        // Determine a random target count between min and max
        int targetCount = Random.Range(minTargetCount, maxTargetCount + 1);
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
        if (targets == null || targets.Length == 0) return;
        foreach (var target in targets)
        {
            if (target != null) Destroy(target);
        }
    }
}

