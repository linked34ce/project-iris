using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleUiManager : MonoBehaviour
{
    [SerializeField] private Image _turnIndicator;
    [SerializeField] private EnemyLoader _enemyLoader;
    [SerializeField] private PlayerPortraitLoader _playerPortraitLoader;
    [SerializeField] private CoroutineController _coroutineController;
    [SerializeField] private CommandWindow _commandWindow;
    [SerializeField] private UiStateManager _uiStateManager;
    [SerializeField] private BattleResult _battleResult;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GameObject _attackersPanel;

    [SerializeField] private PlayerContainer _playerContainer;

    private IPlayer _player;
    private IEnemy _enemy;

    private BattleFlowController _flowController;
    private BattleResultController _resultController;

    private Action _onPlayerTurnBeginHandler;
    private Action _onEnemyTurnBeginHandler;

    private bool _isInitializing = false;

    private readonly Logger _logger = new();

    void Awake()
    {
        _commandWindow.Hide();
        _battleResult.Hide();
        _attackersPanel.SetActive(false);
        _turnIndicator.enabled = false;
        _playerContainer.Initialize();
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
        if (_isInitializing || _flowController is null || _enemy is null)
        {
            return;
        }

        if (_flowController.BattleState == BattleState.Victory
         && !_resultController.BattleResult.IsShown)
        {
            _commandWindow.Hide();
            _resultController.ShowResult();
            DisposeFlowController();
        }
    }

    private async Task Initialize()
    {
        _player = _playerContainer.Player;
        await _playerPortraitLoader.Create();
        _enemy = await _enemyLoader.Create();

        _attackersPanel.SetActive(true);
        _player.Initialize();
        _enemy.Initialize();

        DisposeFlowController();
        _flowController = new BattleFlowController(
            _player,
            _enemy,
            _coroutineController,
            _sceneLoader
        );
        _resultController = new BattleResultController(
            _player,
            _enemy,
            _enemyLoader,
            _battleResult
        );

        _battleResult.Confirmed += () =>
        {
            _battleResult.Hide();
            _turnIndicator.enabled = false;
            _uiStateManager.UiState = UiState.Dungeon;
        };

        _commandWindow.ClearAllEvents();
        SubscribeCommandActions();
        SubscribeTurnEventHandlers();

        _flowController.InitializeBattleState();
    }

    private void DisposeFlowController()
    {
        _flowController?.Dispose();
        _flowController = null;
    }

    private void SubscribeCommandActions() =>
        _commandWindow.SubscribeEachEvent(new Dictionary<Command, UnityAction>
        {
            { Command.Attack, () => _flowController.PlayerAttack(4) },
            { Command.Skill, () => _logger.Debug("SkillButton is selected") },
            { Command.Item, () => _logger.Debug("ItemButton is selected") }
        });

    private void SubscribeTurnEventHandlers()
    {
        if (_onPlayerTurnBeginHandler is not null)
        {
            _flowController.OnPlayerTurnBegin -= _onPlayerTurnBeginHandler;
        }

        if (_onEnemyTurnBeginHandler is not null)
        {
            _flowController.OnEnemyTurnBegin -= _onEnemyTurnBeginHandler;
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

        _flowController.OnPlayerTurnBegin += _onPlayerTurnBeginHandler;
        _flowController.OnEnemyTurnBegin += _onEnemyTurnBeginHandler;
    }
}
