public class Location
{
    public int X { get; set; }
    public int Y { get; set; }

    private readonly Dungeons _dungeons = new();

    public Location()
    {
        int[] initialPosition = _dungeons.InitialPositions[Status.DungeonName][Status.Floor - 1];
        X = initialPosition[0];
        Y = initialPosition[1];
    }

    public void ResetPosition()
    {
        int[] initialPosition = _dungeons.InitialPositions[Status.DungeonName][Status.Floor - 1];
        X = initialPosition[0];
        Y = initialPosition[1];
    }
}
