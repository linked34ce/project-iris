using System;

public class Walls
{
    public Wall East { get; }
    public Wall South { get; }
    public Wall West { get; }
    public Wall North { get; }

    public Walls(int east, int south, int west, int north)
    {
        East = ConvertIntToWall(east);
        South = ConvertIntToWall(south);
        West = ConvertIntToWall(west);
        North = ConvertIntToWall(north);
    }

    public Walls(Wall east, Wall south, Wall west, Wall north)
    {
        East = east;
        South = south;
        West = west;
        North = north;
    }

    private Wall ConvertIntToWall(int x) => Enum.IsDefined(typeof(Wall), x)
                                            ? (Wall)x
                                            : Wall.undefined;

    public Wall GetWall(Direction direction) => direction switch
    {
        Direction.east => East,
        Direction.south => South,
        Direction.west => West,
        Direction.north => North,
        _ => Wall.undefined,
    };
}
