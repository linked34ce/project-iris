public class Location
{
    public int X { get; set; }
    public int Y { get; set; }

    public Location()
    {
        int[] initialPosition = Dungeons.InitialPositions[Status.DungeonName][Status.Floor - 1];
        X = initialPosition[0];
        Y = initialPosition[1];
    }

    public void ResetPosition()
    {
        int[] initialPosition = Dungeons.InitialPositions[Status.DungeonName][Status.Floor - 1];
        X = initialPosition[0];
        Y = initialPosition[1];
    }
}
