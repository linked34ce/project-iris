using TMPro;

using UnityEngine;

public class DungeonUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _locationName;

    public Dungeon Dungeon { get; } = new();

    void Awake() => _locationName.SetText($"{Dungeon.Name} {Status.Floor}F");
}
