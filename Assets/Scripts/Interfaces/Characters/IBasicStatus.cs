public interface IBasicStatus : IBasicParameters
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public bool IsAlive { get; set; }
}
