public interface IPlayer : ICharacter
{
    PlayerData Data { get; }
    void GainExp(IEnemy enemy);
    void ShowResult();
}
