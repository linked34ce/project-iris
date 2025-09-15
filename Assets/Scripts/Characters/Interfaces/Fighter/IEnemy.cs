public interface IEnemy : ICharacter
{
    EnemyData Data { get; }
    void OnAttacked();
}
