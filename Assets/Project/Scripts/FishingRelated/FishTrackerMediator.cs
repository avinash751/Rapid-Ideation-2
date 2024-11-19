using UnityEngine;
using System;

public class FishTrackerMediator : MonoBehaviour
{
    private static FishTrackerMediator _instance;
    public static FishTrackerMediator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<FishTrackerMediator>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("FishTrackerMediator");
                    _instance = obj.AddComponent<FishTrackerMediator>();
                }
            }
            return _instance;
        }
    }

    private int totalFishCollected;

    public event Action<int> OnFishAdded;
    public event Action<int> OnFishRemoved;

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

    public void IncrementFishCount()
    {
        totalFishCollected++;
        OnFishAdded?.Invoke(totalFishCollected);
    }

    public void DecrementFishCount()
    {
        if (totalFishCollected > 0)
        {
            totalFishCollected--;
            OnFishRemoved?.Invoke(totalFishCollected);
        }
    }

    public int GetTotalFishCollected()
    {
        return totalFishCollected;
    }
}
