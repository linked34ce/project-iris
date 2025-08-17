public interface IEnemy : ICharacter
{
    public EnemyData Data { get; }
    public bool IsAttacked { get; set; }
}
