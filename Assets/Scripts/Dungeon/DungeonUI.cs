using TMPro;

using UnityEngine;

public class DungeonUI : SingletonMonoBehaviour<DungeonUI>
{
    [SerializeField] private TMP_Text _locationName;
    public TMP_Text LocationName => _locationName;

    public Dungeon Dungeon { get; } = new();

    protected override void Awake()
    {
        base.Awake();
        LocationName.SetText($"{Dungeon.Name} {Status.Floor}F");
    }
}
