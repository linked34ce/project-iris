public interface IBasicStatus : IBasicParameters
{
    string Name { get; set; }
    int Level { get; set; }
    int Hp { get; set; }
    int MaxHp { get; set; }
    bool IsAlive { get; set; }
}
