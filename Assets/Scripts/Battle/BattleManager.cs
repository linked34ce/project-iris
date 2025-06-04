using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    [SerializeField] private Image _turnBackground;
    public Image TurnBackground => _turnBackground;

    [SerializeField] private Player _player;
    public Player Player => _player;
    [SerializeField] private Enemy _enemy;
    public Enemy Enemy => _enemy;

    public Animator EnemyImageAnimator { get; private set; }
    public Coroutine CurrentCoroutine { get; private set; }

    public bool IsPlayerTurn { get; private set; } = true;
    public bool IsOver { get; private set; } = false;
    public bool HasShownResult { get; private set; } = false;
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
        if (Enemy == null)
        {
            return;
        }

        if (IsOver)
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
                IsOver = true;
                return;
            }

            if (!Player.IsAlive)
            {
                IsOver = true;
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

    void OnEnable()
    {
        Enemy.ResetStatus();
        Enemy.ShowAllStatus();
        Player.ShowAllStatus();
        InitialExp = Player.Exp;
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
        if (EnemyImageAnimator == null)
        {
            EnemyImageAnimator = EnemyImagePrefabManager
                                    .Instance
                                    .GetComponentFromPrefab<Animator>();
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
        CommandWindow.Instance.Hide();
        HasShownResult = true;
    }

    private void ConfirmResult()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IsPlayerTurn = true;
            IsOver = false;
            UIStateManager.Instance.UIState = UIState.Dungeon;
            HasShownResult = false;
        }
    }
}
