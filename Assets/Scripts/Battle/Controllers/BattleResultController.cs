using UnityEngine;

public class BattleResultController
{
    private IPlayer _player;
    private IEnemy _enemy;
    private readonly EnemyLoader _enemyLoader;
    private IBattleResult _battleResult;
    public IBattleResult BattleResult => _battleResult;

    public BattleResultController(
        IPlayer player,
        IEnemy enemy,
        EnemyLoader enemyLoader,
        IBattleResult battleResultView
    )
    {
        _player = player;
        _enemy = enemy;
        _enemyLoader = enemyLoader;
        _battleResult = battleResultView;
    }

    public void ShowResult()
    {
        _player.GainExp(_enemy);

        _enemyLoader.Destroy();
        _enemy = null;

        _battleResult.Show(_player);
    }
}
