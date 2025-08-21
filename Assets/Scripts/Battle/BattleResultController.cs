using UnityEngine;

public class BattleResultController
{
    private IPlayer _player;
    private IEnemy _enemy;
    private readonly EnemyLoader _enemyLoader;
    private readonly BattleFlowController _flowController;
    public bool HasResultShown { get; private set; } = false;

    public BattleResultController(
        IPlayer player,
        IEnemy enemy,
        EnemyLoader enemyLoader,
        BattleFlowController flowController
    )
    {
        _player = player;
        _enemy = enemy;
        _enemyLoader = enemyLoader;
        _flowController = flowController;
    }

    public void ShowResult()
    {
        if (_flowController.BattleState == BattleState.Victory
            && !HasResultShown)
        {
            BattleUIManager.Instance.DisposeFlowController();

            _player.GainExp(_enemy);

            _enemyLoader.Destroy();
            _enemy = null;

            _player.ShowResult();
            CommandWindow.Instance.Hide();
            HasResultShown = true;
        }
    }

    public void ConfirmResult()
    {
        if (HasResultShown && Input.GetKeyDown(KeyCode.Return))
        {
            HasResultShown = false;
            UIStateManager.Instance.UIState = UIState.Dungeon;
        }
    }
}
