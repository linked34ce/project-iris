public interface IPlayer : ICharacter
{
    public PlayerData Data { get; }
    public void GainExp(IEnemy enemy);
    public void ShowResult();
}
