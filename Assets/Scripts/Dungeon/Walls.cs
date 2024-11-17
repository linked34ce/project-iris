public class Walls
{
    public int East { get; }
    public int South { get; }
    public int West { get; }
    public int North { get; }

    public Walls(int east, int south, int west, int north)
    {
        East = east;
        South = south;
        West = west;
        North = north;
    }
}