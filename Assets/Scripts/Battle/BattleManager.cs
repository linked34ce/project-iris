using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public CommandWindow CommandWindow => _commandWindow;
    [SerializeField] private CommandWindow _commandWindow;
    [SerializeField] private Image _turnBackground;
    public Image TurnBackground => _turnBackground;

    [SerializeField] private UIStateManager _uiStateManager;
    public UIStateManager UIStateManager => _uiStateManager;

    [SerializeField] private Player _player;
    public Player Player => _player;
    [SerializeField] private Enemy _enemy;
    public Enemy Enemy => _enemy;

    [SerializeField] private EnemyImagePrefabManager _enemyImagePrefabManager;
    public EnemyImagePrefabManager EnemyImagePrefabManager => _enemyImagePrefabManager;
    public Animator EnemyImageAnimator { get; private set; }

    public Coroutine CurrentCoroutine { get; private set; }
    public bool IsPlayerTurn { get; private set; } = true;
    public bool IsOver { get; private set; } = false;
    public bool HasShownResult { get; private set; } = false;
    public int InitialExp { get; private set; }

    private const float FadeDuration = 0.4f;
    private const string GameOverScene = "Scenes/Menu/GameOver";

    private static BattleManager s_instance;
    public static BattleManager Instance
    {
        get
        {
            if (null == s_instance)
            {
                s_instance = (BattleManager)FindAnyObjectByType(typeof(BattleManager));
                if (null == s_instance)
                {
                    Debug.Log("BattleManager Instance Error");
                }
            }
            return s_instance;
        }
    }

    void Awake() => SetEvents();

    void Update()
    {
        if (Enemy == null)
        {
            return;
        }

        if (IsOver)
        {
            if (Player.Hp > 0)
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
            if (Enemy.Hp <= 0)
            {
                IsOver = true;
                return;
            }

            if (Player.Hp <= 0)
            {
                IsOver = true;
                Initiate.Fade(GameOverScene, Color.black, FadeDuration);
            }
            else
            {
                CommandWindow.PlayButtonSelect();

                if (IsPlayerTurn && !CommandWindow.IsVisible)
                {
                    CommandWindow.Show();
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

    void OnEnable()
    {
        Enemy.ResetStatus();
        Enemy.ShowAllStatus();
        Player.ShowAllStatus();
        InitialExp = Player.Exp;
    }

    private void SetEvents()
    {
        CommandWindow.Commands[Command.Attack].SetEvent(() =>
       {
           CommandWindow.Hide();
           TurnBackground.enabled = false;
           Player.Attack(Enemy);
           if (Enemy.Hp > 0)
           {
               IsPlayerTurn = false;
           }
       });

        CommandWindow.Commands[Command.Skill].SetEvent(() =>
            Debug.Log("SkillButton is selected")
        );

        CommandWindow.Commands[Command.Item].SetEvent(() =>
            Debug.Log("ItemButton is selected")
        );
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1.0f);
        IsPlayerTurn = true;
        Enemy.Attack(Player);
        CurrentCoroutine = null;
    }

    private void WaitForEnemyTurn()
    {
        if (EnemyImageAnimator == null)
        {
            EnemyImageAnimator = EnemyImagePrefabManager.EnemyImageAnimator;
        }

        if (!EnemyImageAnimator.GetBool("isAttacked"))
        {
            CurrentCoroutine = StartCoroutine(EnemyTurn());
        }
    }

    private void ShowResult()
    {
        if (InitialExp == Player.Exp)
        {
            Player.Exp += Enemy.DropExp;
        }

        Enemy.HideImage();
        Player.ShowResult();
        CommandWindow.Hide();
        HasShownResult = true;
    }

    private void ConfirmResult()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IsPlayerTurn = true;
            IsOver = false;
            UIStateManager.UIState = UIState.Dungeon;
            HasShownResult = false;
        }
    }
}
