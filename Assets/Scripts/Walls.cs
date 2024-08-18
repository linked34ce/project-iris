public class Walls
{
    private readonly int east;
    private readonly int south;
    private readonly int west;
    private readonly int north;

    public Walls(int east, int south, int west, int north)
    {
        this.east = east;
        this.south = south;
        this.west = west;
        this.north = north;
    }

    public int GetEast()
    {
        return east;
    }

    public int GetSouth()
    {
        return south;
    }

    public int GetWest()
    {
        return west;
    }
    public int GetNorth()
    {
        return north;
    }
}