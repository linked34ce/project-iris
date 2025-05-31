public class Dungeon
{
    public string Name { get; private set; }
    public float EncountRate { get; private set; }
    public Walls[][] Map { get; private set; }

    public Dungeon()
    {
        Name = Dungeons.DisplayNames[Status.DungeonName];
        EncountRate = Dungeons.EncountRates[Status.DungeonName];
        Map = Dungeons.Maps[Status.DungeonName][Status.Floor - 1];
    }
}