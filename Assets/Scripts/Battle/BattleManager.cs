using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject dungeonUi;
    public GameObject DungeonUi => dungeonUi;

    [SerializeField] private GameObject battleUi;
    public GameObject BattleUi => battleUi;

    [SerializeField] private Button attackButton;
    public Button AttackButton => attackButton;
    [SerializeField] private Button skillButton;
    public Button SkillButton => skillButton;
    [SerializeField] private Button itemButton;
    public Button ItemButton => itemButton;

    public GameObject SelectedButton { get; set; }
    public GameObject CommandWindow { get; private set; }
    public GameObject TurnBackground { get; private set; }
    public Enemy Enemy { get; private set; }
    public Player Player { get; private set; }
    public Coroutine CurrentCoroutine { get; private set; }
    public bool IsFirstEnabled { get; set; } = true;
    public bool IsPlayerTurn { get; private set; } = true;
    public bool IsOver { get; private set; } = false;
    public int InitialExp { get; private set; }

    void Awake()
    {
        Player = new("歩夢", "healer", 0, 1);
        CommandWindow = GameObject.Find("/BattleUI/Attacker1Commands");
        TurnBackground = GameObject.Find("/BattleUI/Attackers/Attacker1/Portrait/Turn");
        SelectedButton = EventSystem.current.currentSelectedGameObject;

        AttackButton.onClick.AddListener(() =>
        {
            CommandWindow.SetActive(false);
            TurnBackground.SetActive(false);
            Player.Attack(Enemy);
            if (Enemy.Hp > 0)
            {
                IsPlayerTurn = false;
            }
        });

        SkillButton.onClick.AddListener(() =>
        {
            Debug.Log("SkillButton is selected");
        });

        ItemButton.onClick.AddListener(() =>
        {
            Debug.Log("SkillButton is selected");
        });
    }

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
                CommandWindow.SetActive(false);
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
                Initiate.Fade("Scenes/Menu/GameOver", Color.black, 0.4f);
            }
            else
            {
                PlayButtonSelect();

                if (IsPlayerTurn && !CommandWindow.activeSelf)
                {
                    CommandWindow.SetActive(true);
                    TurnBackground.SetActive(true);
                }
                else if (!IsPlayerTurn && !GameObject.Find("/BattleUI/EnemyImage").GetComponent<Animator>().GetBool("isAttacked") && CurrentCoroutine == null)
                {
                    CurrentCoroutine = StartCoroutine(EnemyTurn());
                }
            }
        }
    }

    void OnEnable()
    {
        DungeonUi.SetActive(false);
        BattleUi.SetActive(true);
        GetComponent<CameraController>().enabled = false;
        if (IsFirstEnabled)
        {
            IsFirstEnabled = false;
        }
        else
        {
            Enemy = new("コモン・テラン", "tsuchinoko", 10, 2, 10, 3, 1, 2, 1, 1, 3);
            Enemy.ShowAllStatus();
        }
        Player.ShowAllStatus();
        InitialExp = Player.Exp;
    }

    void OnDisable()
    {
        Enemy = null;
        GetComponent<CameraController>().enabled = true;
        if (DungeonUi && BattleUi)
        {
            DungeonUi.SetActive(true);
            BattleUi.SetActive(false);
        }
    }

    private void PlayButtonSelect()
    {
        if (SelectedButton != EventSystem.current.currentSelectedGameObject)
        {
            SelectedButton = EventSystem.current.currentSelectedGameObject;
            GetComponent<BattleUISounds>().PlayButtonSelect();
        }
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
            Enemy.Hp = Enemy.MaxHp;
            IsPlayerTurn = true;
            IsOver = false;
            enabled = false;
        }
    }
}