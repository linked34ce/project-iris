using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public CommandWindow CommandWindow => _commandWindow;
    [SerializeField] private CommandWindow _commandWindow;
    [SerializeField] private Image _turnBackground;
    public Image TurnBackground => _turnBackground;
    [SerializeField] private RawImage _enemyImage;
    public RawImage EnemyImage => _enemyImage;
    [SerializeField] private Animator _enemyImageAnimator;
    public Animator EnemyImageAnimator => _enemyImageAnimator;

    [SerializeField] private UIStateManager _uiStateManager;
    public UIStateManager UIStateManager => _uiStateManager;

    [SerializeField] private Player _player;
    public Player Player => _player;
    [SerializeField] private Enemy _enemy;
    public Enemy Enemy => _enemy;

    public Coroutine CurrentCoroutine { get; private set; }
    public bool IsPlayerTurn { get; private set; } = true;
    public bool IsOver { get; private set; } = false;
    public int InitialExp { get; private set; }

    private const float FADE_DURATION = 0.4f;

    private static BattleManager instance;

    public static BattleManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = (BattleManager)FindAnyObjectByType(typeof(BattleManager));
                if (null == instance)
                {
                    Debug.Log("BattleManager Instance Error");
                }
            }
            return instance;
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
                ShowResult();
                CommandWindow.Hide();
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
                Initiate.Fade("Scenes/Menu/GameOver", Color.black, FADE_DURATION);
            }
            else
            {
                CommandWindow.PlayButtonSelect();

                if (IsPlayerTurn && !CommandWindow.IsVisible)
                {
                    CommandWindow.Show();
                    TurnBackground.enabled = true;
                }
                else if (!IsPlayerTurn && !EnemyImageAnimator.GetBool("isAttacked") && CurrentCoroutine is null)
                {
                    CurrentCoroutine = StartCoroutine(EnemyTurn());
                }
            }
        }
    }

    void OnEnable()
    {
        Enemy.ResetStatus();
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

    public void ShowResult()
    {
        if (InitialExp == Player.Exp)
        {
            Player.Exp += Enemy.DropExp;
        }

        Enemy.HideImage();
        Player.ShowResult();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            IsPlayerTurn = true;
            IsOver = false;
            UIStateManager.UIState = UIState.Dungeon;
        }
    }
}