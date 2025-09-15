using System.Threading.Tasks;

public class BattleResultController
{
    private IPlayer _player;
    private IEnemy _enemy;
    private readonly IPrefabLoader<Task<IEnemy>> _enemyLoader;
    private readonly IBattleResult _battleResult;
    public IBattleResult BattleResult => _battleResult;

    public BattleResultController(
        IPlayer player,
        IEnemy enemy,
        IPrefabLoader<Task<IEnemy>> enemyLoader,
        IBattleResult battleResult
    )
    {
        _player = player;
        _enemy = enemy;
        _enemyLoader = enemyLoader;
        _battleResult = battleResult;
    }

    public void ShowResult()
    {
        _player.GainExp(_enemy);
        _enemyLoader.Destroy();
        _battleResult.Show(_player);
    }
}
