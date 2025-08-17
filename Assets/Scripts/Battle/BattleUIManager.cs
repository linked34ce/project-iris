using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : SingletonMonoBehaviour<BattleUIManager>
{
    [SerializeField] private Image _turnBackground;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyLoader _enemyLoader;

    public IEnemy Enemy { get; private set; }
    public Coroutine CurrentCoroutine { get; private set; }

    public BattleFlowController FlowController { get; private set; }

    private const float FadeDuration = 0.4f;
    private const string GameOverScene = "Scenes/Menu/GameOver";

    protected override void Awake()
    {
        base.Awake();
        _player.Initialize();
        SetEvents();
    }

    void Update()
    {
        switch (FlowController.BattleState)
        {
            case BattleState.InBattle:
                ExecuteBattlePhase();
                break;
            case BattleState.HasPlayerWon:
                ShowResult();
                break;
            case BattleState.HasShownResult:
                ConfirmResult();
                break;
        }
    }

    async void OnEnable()
    {
        Enemy = await _enemyLoader.Create();
        _player.ShowAllStatus();
        Enemy.Initialize();
        FlowController = new BattleFlowController(_player, Enemy);
    }

    void OnDisable() => FlowController.DisposeBattleState();

    private void SetEvents()
    {
        CommandWindow.Instance.Commands[Command.Attack].SetEvent(() =>
       {
           CommandWindow.Instance.Hide();
           _turnBackground.enabled = false;
           FlowController.PlayerAttack(4);
       });

        CommandWindow.Instance.Commands[Command.Skill].SetEvent(() =>
            Debug.Log("SkillButton is selected")
        );

        CommandWindow.Instance.Commands[Command.Item].SetEvent(() =>
            Debug.Log("ItemButton is selected")
        );
    }

    private void ExecuteBattlePhase()
    {
        // When the player and the enemy are defeated at the same time, it's game over.
        if (!_player.Data.IsAlive)
        {
            LoadGameOverScene();
        }
        else if (!Enemy.Data.IsAlive)
        {
            FlowController.PlayerHasWon();
        }
        else
        {
            OnPlayerTurn();
        }
    }

    private void OnPlayerTurn()
    {
        CommandWindow.Instance.PlayButtonSelect();

        if (FlowController.Turn == Turn.Player && !CommandWindow.Instance.IsVisible)
        {
            CommandWindow.Instance.Show();
            _turnBackground.enabled = true;
        }
        else if (
            FlowController.Turn == Turn.Enemy
            && CurrentCoroutine is null
        )
        {
            WaitForEnemyTurn();
        }
    }

    private IEnumerator OnEnemyTurn()
    {
        yield return new WaitForSeconds(1.0f);
        FlowController.EnemyAttack(5);
        CurrentCoroutine = null;
    }

    private void WaitForEnemyTurn()
    {
        if (!Enemy.IsAttacked)
        {
            CurrentCoroutine = StartCoroutine(OnEnemyTurn());
        }
    }

    private void LoadGameOverScene()
    {
        FlowController.DisposeBattleState();
        Initiate.Fade(GameOverScene, Color.black, FadeDuration);
    }

    private void ShowResult()
    {
        _player.GainExp(Enemy);
        _enemyLoader.Destroy();
        _player.ShowResult();
        CommandWindow.Instance.Hide();
        FlowController.ResultHasShown();
    }

    private void ConfirmResult()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FlowController.DisposeBattleState();
            UIStateManager.Instance.UIState = UIState.Dungeon;
        }
    }
}
