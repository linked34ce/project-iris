using System.Collections;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    [SerializeField] private Image _turnBackground;
    public Image TurnBackground => _turnBackground;

    [SerializeField] private Player _player;
    public Player Player => _player;
    [SerializeField] private string _imageAddress;
    public string ImageAddress => _imageAddress;

    public Enemy Enemy { get; private set; }
    public Coroutine CurrentCoroutine { get; private set; }

    public BattleState BattleState { get; private set; } = BattleState.None;
    public Turn Turn { get; private set; } = Turn.None;

    private const float FadeDuration = 0.4f;
    private const string GameOverScene = "Scenes/Menu/GameOver";

    protected override void Awake()
    {
        base.Awake();
        SetEvents();
    }

    void Update()
    {
        switch (BattleState)
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
        Enemy = await LoadEnemy();
        Enemy.ShowAllStatus();
        Player.ShowAllStatus();
        ResetBattleState();
    }

    void OnDisable()
    {
        BattleState = BattleState.None;
    }

    private async Task<Enemy> LoadEnemy()
    {
        await EnemyPrefabManager.Instance.LoadPrefab(ImageAddress);
        return EnemyPrefabManager.Instance.GetComponentFromPrefab<Enemy>();
    }

    // OnBattle, HasWon and HasShownResult flags should be changed to an enum type
    private void ResetBattleState()
    {
        BattleState = BattleState.InBattle;
        Turn = Turn.Player;
    }

    private void SetEvents()
    {
        CommandWindow.Instance.Commands[Command.Attack].SetEvent(() =>
       {
           CommandWindow.Instance.Hide();
           TurnBackground.enabled = false;
           Player.Attack(Enemy, 4);
           if (Enemy.IsAlive)
           {
               Turn = Turn.Enemy;
           }
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
        if (!Player.IsAlive)
        {
            LoadGameOverScene();
        }
        else if (!Enemy.IsAlive)
        {
            BattleState = BattleState.HasPlayerWon;
        }
        else
        {
            OnPlayerTurn();
        }
    }

    private void OnPlayerTurn()
    {
        CommandWindow.Instance.PlayButtonSelect();

        if (Turn == Turn.Player && !CommandWindow.Instance.IsVisible)
        {
            CommandWindow.Instance.Show();
            TurnBackground.enabled = true;
        }
        else if (
            Turn == Turn.Enemy
            && CurrentCoroutine is null
        )
        {
            WaitForEnemyTurn();
        }
    }

    private IEnumerator OnEnemyTurn()
    {
        yield return new WaitForSeconds(1.0f);
        Turn = Turn.Player;
        Enemy.Attack(Player, 5);
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
        BattleState = BattleState.None;
        Initiate.Fade(GameOverScene, Color.black, FadeDuration);
    }

    private void ShowResult()
    {
        Player.Exp += Enemy.DropExp;
        EnemyPrefabManager.Instance.DestroyPrefab();
        Player.ShowResult();
        CommandWindow.Instance.Hide();
        BattleState = BattleState.HasShownResult;
    }

    private void ConfirmResult()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            BattleState = BattleState.None;
            UIStateManager.Instance.UIState = UIState.Dungeon;
        }
    }
}
