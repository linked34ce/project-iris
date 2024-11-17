using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject dungeonUi;
    public GameObject DungeonUi => dungeonUi;

    [SerializeField] private GameObject battleUi;
    public GameObject BattleUi => battleUi;

    public Enemy Enemy { get; } = new("コモン・テラン", "tsuchinoko", 10, 2, 10, 3, 1, 2, 1, 1, 3);
    public Player Player { get; } = new("歩夢", "healer", 0, 1);
    public bool IsPlayerTurn { get; private set; } = true;
    public bool IsOver { get; private set; } = false;
    public BasicCommand SelectedCommand { get; private set; } = BasicCommand.attack;
    public int InitialExp { get; private set; }

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

            GameObject turnIcon = GameObject.Find("/Battle UI/Panel/Attacker 1/Basic/Turn");
            GameObject commandWindow = GameObject.Find("/Battle UI/Panel/Attacker 1/Commands");

            if (Player.Hp <= 0)
            {
                IsOver = true;
                Initiate.Fade("Scenes/Menu/Game Over", Color.black, 0.4f);
            }
            else
            {
                if (IsPlayerTurn)
                {
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

                turnIcon.SetActive(IsPlayerTurn);
                commandWindow.SetActive(IsPlayerTurn);
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
        GameObject attackArrow = GameObject.Find("/Battle UI/Panel/Attacker 1/Commands/Attack/Arrow");
        GameObject skillsArrow = GameObject.Find("/Battle UI/Panel/Attacker 1/Commands/Skills/Arrow");
        GameObject itemsArrow = GameObject.Find("/Battle UI/Panel/Attacker 1/Commands/Items/Arrow");

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