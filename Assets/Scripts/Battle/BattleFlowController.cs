public class BattleFlowController
{
    private readonly IPlayer _player;
    private readonly IEnemy _enemy;

    public BattleState BattleState { get; private set; }
    public Turn Turn { get; private set; }

    public BattleFlowController(IPlayer player, IEnemy enemy)
    {
        _player = player;
        _enemy = enemy;
        InitializeBattleState();
    }

    public void DisposeBattleState() => BattleState = BattleState.None;

    public void PlayerHasWon() => BattleState = BattleState.HasPlayerWon;

    public void ResultHasShown() => BattleState = BattleState.HasShownResult;

    private void InitializeBattleState()
    {
        BattleState = BattleState.InBattle;
        Turn = Turn.Player;
    }

    public void PlayerAttack(int damage)
    {
        _player.Attack(_enemy, damage);
        if (_enemy.Data.IsAlive)
        {
            Turn = Turn.Enemy;
        }
    }

    public void EnemyAttack(int damage)
    {
        _enemy.Attack(_player, damage);
        Turn = Turn.Player;
    }
}
