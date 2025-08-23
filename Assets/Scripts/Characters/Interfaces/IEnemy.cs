public interface IEnemy : ICharacter
{
    EnemyData Data { get; }
    bool IsAttacked { get; set; }
}
