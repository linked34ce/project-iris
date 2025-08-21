using System;
using System.Collections;

using UnityEngine;

public class BattleFlowController
{
    private readonly IPlayer _player;
    private readonly IEnemy _enemy;
    private readonly ICoroutineController _coroutineController;

    private Turn _turn = Turn.None;
    public Turn Turn
    {
        get => _turn;
        private set
        {
            if (_turn != value)
            {
                _turn = value;
                if (_turn == Turn.Player)
                {
                    OnPlayerTurnBegin?.Invoke();
                }
                else if (_turn == Turn.Enemy)
                {
                    OnEnemyTurnBegin?.Invoke();
                }
            }
        }
    }

    private BattleState _battleState = BattleState.None;
    public BattleState BattleState
    {
        get => _battleState;
        set
        {
            if (_battleState != value)
            {
                _battleState = value;
                if (_battleState == BattleState.GameOver)
                {
                    LoadGameOverScene();
                }
            }
        }
    }

    public Action OnPlayerTurnBegin;
    public Action OnEnemyTurnBegin;

    private const float FadeDuration = 0.4f;
    private const string GameOverScene = "Scenes/Menu/GameOver";

    public BattleFlowController(
        IPlayer player,
        IEnemy enemy,
        ICoroutineController coroutineController
    )
    {
        _player = player;
        _enemy = enemy;
        _coroutineController = coroutineController;
    }

    public void Dispose()
    {
        _coroutineController?.Stop();
        CommandWindow.Instance.ClearAllEvents();

        OnPlayerTurnBegin = null;
        OnEnemyTurnBegin = null;

        BattleState = BattleState.None;
        Turn = Turn.None;
    }

    public void InitializeBattleState()
    {
        BattleState = BattleState.InBattle;
        Turn = Turn.Player;
    }

    public void EvaluateBattleState()
    {
        if (_player.Data.IsAlive && _enemy.Data.IsAlive)
        {
            BattleState = BattleState.InBattle;
        }
        else if (!_player.Data.IsAlive)
        {
            BattleState = BattleState.GameOver;
        }
        else if (!_enemy.Data.IsAlive)
        {
            BattleState = BattleState.Victory;
        }
    }

    public void PlayCommandSelectSound() => CommandWindow.Instance.PlayButtonSelect();

    private IEnumerator OnEnemyTurn()
    {
        yield return new WaitForSeconds(1.0f);
        EnemyAttack(5);
        _coroutineController.Stop();
    }

    private void LoadGameOverScene() => Initiate.Fade(GameOverScene, Color.black, FadeDuration);

    public void PlayerAttack(int damage)
    {
        _player.Attack(_enemy, damage);
        if (_enemy.Data.IsAlive)
        {
            Turn = Turn.Enemy;
            _coroutineController.Begin(OnEnemyTurn());
        }
    }

    private void EnemyAttack(int damage)
    {
        _enemy.Attack(_player, damage);
        Turn = Turn.Player;
    }
}
