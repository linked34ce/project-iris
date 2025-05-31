using TMPro;

using UnityEngine;

public class DungeonUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _locationName;
    public TMP_Text LocationName => _locationName;

    public Dungeon Dungeon { get; } = new();

    void Awake()
    {
        LocationName.SetText($"{Dungeon.Name} {Status.Floor}F");
    }
}
