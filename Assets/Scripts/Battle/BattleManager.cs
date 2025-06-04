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

    public bool IsPlayerTurn { get; private set; } = true;
    public bool HasWon { get; private set; } = false;
    public bool HasShownResult { get; private set; } = false;
    public bool OnBattle { get; set; }
    public int InitialExp { get; private set; }

    private const float FadeDuration = 0.4f;
    private const string GameOverScene = "Scenes/Menu/GameOver";

    protected override void Awake()
    {
        base.Awake();
        SetEvents();
    }

    void Update()
    {
        if (!OnBattle)
        {
            return;
        }

        if (HasWon)
        {
            if (Player.IsAlive)
            {
                if (HasShownResult)
                {
                    ConfirmResult();
                }
                else
                {
                    ShowResult();
                }
            }
        }
        else
        {
            if (!Enemy.IsAlive)
            {
                HasWon = true;
                return;
            }

            if (!Player.IsAlive)
            {
                OnBattle = false;
                Initiate.Fade(GameOverScene, Color.black, FadeDuration);
            }
            else
            {
                CommandWindow.Instance.PlayButtonSelect();

                if (IsPlayerTurn && !CommandWindow.Instance.IsVisible)
                {
                    CommandWindow.Instance.Show();
                    TurnBackground.enabled = true;
                }
                else if (
                    !IsPlayerTurn
                    && CurrentCoroutine is null
                )
                {
                    WaitForEnemyTurn();
                }
            }
        }
    }

    async void OnEnable()
    {
        Enemy = await LoadEnemy();
        Enemy.ShowAllStatus();
        Player.ShowAllStatus();
        InitialExp = Player.Exp;
        ResetBattleState();
    }

    void OnDisable()
    {
        OnBattle = false;
    }

    private async Task<Enemy> LoadEnemy()
    {
        await EnemyPrefabManager.Instance.LoadPrefab(ImageAddress);
        return EnemyPrefabManager.Instance.GetComponentFromPrefab<Enemy>();
    }

    private void ResetBattleState()
    {
        OnBattle = true;
        HasWon = false;
        IsPlayerTurn = true;
    }

    private void SetEvents()
    {
        CommandWindow.Instance.Commands[Command.Attack].SetEvent(() =>
       {
           CommandWindow.Instance.Hide();
           TurnBackground.enabled = false;
           Player.Attack(Enemy, 4);
           if (Enemy.Hp > 0)
           {
               IsPlayerTurn = false;
           }
       });

        CommandWindow.Instance.Commands[Command.Skill].SetEvent(() =>
            Debug.Log("SkillButton is selected")
        );

        CommandWindow.Instance.Commands[Command.Item].SetEvent(() =>
            Debug.Log("ItemButton is selected")
        );
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1.0f);
        IsPlayerTurn = true;
        Enemy.Attack(Player, 5);
        CurrentCoroutine = null;
    }

    private void WaitForEnemyTurn()
    {
        if (!Enemy.IsAttacked)
        {
            CurrentCoroutine = StartCoroutine(EnemyTurn());
        }
    }

    private void ShowResult()
    {
        Player.Exp += Enemy.DropExp;
        EnemyPrefabManager.Instance.DestroyPrefab();
        Player.ShowResult();
        CommandWindow.Instance.Hide();
        HasShownResult = true;
    }

    private void ConfirmResult()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HasShownResult = false;
            UIStateManager.Instance.UIState = UIState.Dungeon;
        }
    }
}
