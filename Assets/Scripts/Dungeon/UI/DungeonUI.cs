using TMPro;

using UnityEngine;

public class DungeonUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _locationName;

    private Dungeon _dungeon = new();

    void Awake() => _locationName.SetText($"{_dungeon.Name} {Status.Floor}F");
}
