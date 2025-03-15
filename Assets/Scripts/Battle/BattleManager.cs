using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject dungeonUi;
    public GameObject DungeonUi => dungeonUi;

    [SerializeField] private GameObject battleUi;
    public GameObject BattleUi => battleUi;

    public GameObject CommandWindow { get; private set; }
    public GameObject TurnBackground { get; private set; }
    public Enemy Enemy { get; } = new("コモン・テラン", "tsuchinoko", 10, 2, 10, 3, 1, 2, 1, 1, 3);
    public Player Player { get; } = new("歩夢", "healer", 0, 1);
    public bool IsPlayerTurn { get; private set; } = true;
    public bool IsOver { get; private set; } = false;
    public BasicCommand SelectedCommand { get; private set; } = BasicCommand.attack;
    public int InitialExp { get; private set; }

    void Awake()
    {
        CommandWindow = GameObject.Find("/BattleUI/Attacker1Commands");
        TurnBackground = GameObject.Find("/BattleUI/Attackers/Attacker1/Portrait/Turn");
    }

    void Update()
    {
        if (IsOver)
        {
            if (Player.Hp > 0)
            {
                ShowResult();
            }
        }
        else
        {
            if (Enemy.Hp <= 0)
            {
                IsOver = true;
            }

            if (Player.Hp <= 0)
            {
                IsOver = true;
                Initiate.Fade("Scenes/Menu/GameOver", Color.black, 0.4f);
            }
            else
            {
                if (IsPlayerTurn)
                {
                    CommandWindow.SetActive(IsPlayerTurn);
                    SelectBasicCommand();
                    ExecuteBasicCommand();
                }
                else
                {

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Enemy.Attack(Player);
                        IsPlayerTurn = !IsPlayerTurn;
                    }
                }
                CommandWindow.SetActive(IsPlayerTurn);
                TurnBackground.SetActive(IsPlayerTurn);
            }
        }
    }

    void OnEnable()
    {
        GetComponent<CameraController>().enabled = false;
        DungeonUi.SetActive(false);
        BattleUi.SetActive(true);
        InitialExp = Player.Exp;
        Enemy.ShowAllStatus();
        Player.ShowAllStatus();
    }

    void OnDisable()
    {
        GetComponent<CameraController>().enabled = true;
        if (DungeonUi && BattleUi)
        {
            DungeonUi.SetActive(true);
            BattleUi.SetActive(false);
        }
    }

    public void SelectBasicCommand()
    {
        GameObject attackArrow = GameObject.Find("/BattleUI/Attacker1Commands/Attack/Arrow");
        GameObject skillsArrow = GameObject.Find("/BattleUI/Attacker1Commands/Skills/Arrow");
        GameObject itemsArrow = GameObject.Find("/BattleUI/Attacker1Commands/Items/Arrow");

        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectedCommand = SelectedCommand == BasicCommand.attack ? BasicCommand.items : SelectedCommand - 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SelectedCommand = SelectedCommand == BasicCommand.items ? BasicCommand.attack : SelectedCommand + 1;
        }

        attackArrow.SetActive(SelectedCommand == BasicCommand.attack);
        skillsArrow.SetActive(SelectedCommand == BasicCommand.skills);
        itemsArrow.SetActive(SelectedCommand == BasicCommand.items);
    }

    public void ExecuteBasicCommand()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (SelectedCommand)
            {
                case BasicCommand.attack:
                    Player.Attack(Enemy);
                    IsPlayerTurn = !IsPlayerTurn;
                    break;
                case BasicCommand.skills:
                    break;
                case BasicCommand.items:
                    break;
            }
        }
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