public class Dungeon
{
    public string Name { get; }
    public float EncountRate { get; }
    public Walls[][] Map { get; }


    public Dungeon()
    {
        Name = Dungeons.DisplayNames[Status.DungeonName];
        EncountRate = Dungeons.EncountRates[Status.DungeonName];
        Map = Dungeons.Maps[Status.DungeonName][Status.Floor - 1];
    }
}