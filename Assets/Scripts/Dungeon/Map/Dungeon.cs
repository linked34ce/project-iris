public class Dungeon
{
    public string Name { get; private set; }
    public float EncountRate { get; private set; }
    public Walls[][] Map { get; private set; }
    private readonly Dungeons _dungeons = new();

    public Dungeon()
    {
        Name = _dungeons.DisplayNames[Status.DungeonName];
        EncountRate = _dungeons.EncountRates[Status.DungeonName];
        Map = _dungeons.Maps[Status.DungeonName][Status.Floor - 1];
    }
}
