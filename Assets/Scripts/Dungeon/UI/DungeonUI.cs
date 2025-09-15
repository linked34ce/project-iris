using TMPro;

using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class DungeonUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _locationName;

    private readonly Dungeon _dungeon = new();

    void Awake() => _locationName.SetText($"{_dungeon.Name} {Status.Floor}F");
}
