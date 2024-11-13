using UnityEngine;

public class Fish : MonoBehaviour
{
    [field: SerializeField] public string FishName { get; private set; }
    [field: SerializeField] public Sprite FishSprite { get; private set; }

    public void Initialize(string name, Sprite sprite)
    {
        FishName = name;
        FishSprite = sprite;
    }
}
