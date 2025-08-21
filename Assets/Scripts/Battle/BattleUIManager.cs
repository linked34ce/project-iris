using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleUIManager : SingletonMonoBehaviour<BattleUIManager>
{
    [SerializeField] private Image _turnIndicator;
    public Image TurnIndicator
    {
        get => _turnIndicator;
        set => _turnIndicator = value;
    }

    [SerializeField] private Player _player;
    [SerializeField] private EnemyLoader _enemyLoader;
    [SerializeField] private CoroutineController _coroutineController;

    public IEnemy Enemy { get; private set; }

    public BattleFlowController FlowController { get; set; }
    private BattleResultController _resultController;

    private Action _onPlayerTurnBeginHandler;
    private Action _onEnemyTurnBeginHandler;

    private bool _isInitializing = false;

    protected override void Awake()
    {
        base.Awake();
        _player.Initialize();
    }

    async void OnEnable()
    {
        if (_isInitializing)
        {
            return;
        }

        _isInitializing = true;

        try
        {
            await Initialize();
        }
        finally
        {
            _isInitializing = false;
        }
    }

    void Update()
    {
        if (_isInitializing)
        {
            return;
        }

        if (FlowController is null || Enemy is null)
        {
            _resultController?.ConfirmResult();
            return;
        }

        _resultController.ShowResult();

        if (FlowController?.BattleState == BattleState.InBattle)
        {
            FlowController.EvaluateBattleState();
            FlowController.PlayCommandSelectSound();
        }
    }

    private async Task Initialize()
    {
        _enemyLoader.Initialize();
        Enemy = await _enemyLoader.Create();
        Enemy.Initialize();

        _player.ShowAllStatus();

        DisposeFlowController();
        FlowController = new BattleFlowController(_player, Enemy, _coroutineController);
        _resultController = new BattleResultController(_player, Enemy, _enemyLoader, FlowController);

        CommandWindow.Instance.ClearAllEvents();
        SubscribeCommandActions();
        SubscribeTurnEventHandlers();

        FlowController.InitializeBattleState();
    }

    public void DisposeFlowController()
    {
        FlowController?.Dispose();
        FlowController = null;
    }

    private void SubscribeCommandActions() =>
        CommandWindow.Instance.SubscribeEachEvent(new Dictionary<Command, UnityAction>
        {
            { Command.Attack, () => FlowController.PlayerAttack(4) },
            { Command.Skill, () => Debug.Log("SkillButton is selected") },
            { Command.Item, () => Debug.Log("ItemButton is selected") }
        });

    private void SubscribeTurnEventHandlers()
    {
        if (_onPlayerTurnBeginHandler is not null)
        {
            FlowController.OnPlayerTurnBegin -= _onPlayerTurnBeginHandler;
        }

        if (_onEnemyTurnBeginHandler is not null)
        {
            FlowController.OnEnemyTurnBegin -= _onEnemyTurnBeginHandler;
        }

        _onPlayerTurnBeginHandler = () =>
        {
            CommandWindow.Instance.Show();
            TurnIndicator.enabled = true;
        };

        _onEnemyTurnBeginHandler = () =>
        {
            CommandWindow.Instance.Hide();
            TurnIndicator.enabled = false;
        };

        FlowController.OnPlayerTurnBegin += _onPlayerTurnBeginHandler;
        FlowController.OnEnemyTurnBegin += _onEnemyTurnBeginHandler;
    }
}
