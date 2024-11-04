public class Location
{
    public int X { get; set; }
    public int Y { get; set; }

    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void IncrementX() => X++;

    public void IncrementY() => Y++;

    public void DecrementX() => X--;

    public void DecrementY() => Y--;
}