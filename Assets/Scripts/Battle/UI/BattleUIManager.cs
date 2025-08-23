using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private Image _turnIndicator;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyLoader _enemyLoader;
    [SerializeField] private PlayerPortraitLoader _playerPortraitLoader;
    [SerializeField] private CoroutineController _coroutineController;
    [SerializeField] private CommandWindow _commandWindow;
    [SerializeField] private UIStateManager _uiStateManager;
    [SerializeField] private BattleResult _battleResult;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GameObject _attackersPanel;

    public IEnemy Enemy { get; private set; }

    public BattleFlowController FlowController { get; set; }
    private BattleResultController _resultController;

    private Action _onPlayerTurnBeginHandler;
    private Action _onEnemyTurnBeginHandler;

    private bool _isInitializing = false;

    void Awake()
    {
        _commandWindow.Hide();
        _battleResult.Hide();
        _attackersPanel.SetActive(false);
        _turnIndicator.enabled = false;
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
        if (_isInitializing || FlowController is null || Enemy is null)
        {
            return;
        }

        if (FlowController.BattleState == BattleState.Victory
         && !_resultController.BattleResult.IsShown)
        {
            _commandWindow.Hide();
            _resultController.ShowResult();
            DisposeFlowController();
        }
    }

    private async Task Initialize()
    {
        await _playerPortraitLoader.Create();
        Enemy = await _enemyLoader.Create();

        _attackersPanel.SetActive(true);
        _player.Initialize();
        Enemy.Initialize();

        DisposeFlowController();
        FlowController = new BattleFlowController(
            _player,
            Enemy,
            _coroutineController,
            _sceneLoader
        );
        _resultController = new BattleResultController(
            _player,
            Enemy,
            _enemyLoader,
            _battleResult);

        _battleResult.Confirmed += () =>
        {
            _battleResult.Hide();
            _turnIndicator.enabled = false;
            _uiStateManager.UIState = UIState.Dungeon;
        };

        _commandWindow.ClearAllEvents();
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
        _commandWindow.SubscribeEachEvent(new Dictionary<Command, UnityAction>
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
            _commandWindow.Show();
            _turnIndicator.enabled = true;
        };

        _onEnemyTurnBeginHandler = () =>
        {
            _commandWindow.Hide();
            _turnIndicator.enabled = false;
        };

        FlowController.OnPlayerTurnBegin += _onPlayerTurnBeginHandler;
        FlowController.OnEnemyTurnBegin += _onEnemyTurnBeginHandler;
    }
}
