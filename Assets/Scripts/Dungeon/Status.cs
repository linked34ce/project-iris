public class Status
{
    public static string DungeonName { get; set; } = "To-o Gakuen";
    public static int Floor { get; private set; } = 1;

    public static void IncrementFloor() => Floor++;
}