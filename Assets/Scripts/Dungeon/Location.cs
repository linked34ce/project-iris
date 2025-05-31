public class Location
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Location()
    {
        int[] initialPosition = Dungeons.InitialPositions[Status.DungeonName][Status.Floor - 1];
        X = initialPosition[0];
        Y = initialPosition[1];
    }

    public void IncrementX() => X++;

    public void IncrementY() => Y++;

    public void DecrementX() => X--;

    public void DecrementY() => Y--;

    public void ResetPosition()
    {
        int[] initialPosition = Dungeons.InitialPositions[Status.DungeonName][Status.Floor - 1];
        X = initialPosition[0];
        Y = initialPosition[1];
    }
}
