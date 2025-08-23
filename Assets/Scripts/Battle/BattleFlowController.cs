using System;
using System.Collections;

using UnityEngine;

public class BattleFlowController
{
    private readonly IPlayer _player;
    private readonly IEnemy _enemy;
    private readonly ICoroutineController _coroutineController;
    private readonly WaitForSeconds _waitForSeconds = new(1f);
    private readonly ISceneLoader _sceneLoader;

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
                    _sceneLoader.LoadScene(GameOverScene);
                }
            }
        }
    }

    public Action OnPlayerTurnBegin;
    public Action OnEnemyTurnBegin;

    private const string GameOverScene = "Scenes/Menu/GameOver";

    public BattleFlowController(
        IPlayer player,
        IEnemy enemy,
        ICoroutineController coroutineController,
        ISceneLoader sceneLoader
    )
    {
        _player = player;
        _enemy = enemy;
        _coroutineController = coroutineController;
        _sceneLoader = sceneLoader;
    }

    public void Dispose()
    {
        _coroutineController?.Stop();

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

    private void EvaluateBattleState()
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

    private IEnumerator OnEnemyTurn()
    {
        yield return _waitForSeconds;
        EnemyAttack(5);
        _coroutineController.Stop();
    }

    public void PlayerAttack(int damage)
    {
        _player.Attack(_enemy, damage);
        if (_enemy.Data.IsAlive)
        {
            Turn = Turn.Enemy;
            _coroutineController.Begin(OnEnemyTurn());
        }
        EvaluateBattleState();
    }

    private void EnemyAttack(int damage)
    {
        _enemy.Attack(_player, damage);
        Turn = Turn.Player;
        EvaluateBattleState();
    }
}
