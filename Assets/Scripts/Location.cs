public class Location
{
    private int x;
    private int y;

    public Location(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public void IncrementX()
    {
        x++;
    }

    public void IncrementY()
    {
        y++;
    }

    public void DecrementX()
    {
        x--;
    }

    public void DecrementY()
    {
        y--;
    }
}