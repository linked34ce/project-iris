using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject dungeonUi;
    [SerializeField] GameObject battleUi;
    private readonly Enemy enemy = new("コモン・テラン", "tsuchinoko", 10, 2, 10);
    private readonly Player player = new("歩夢", "healer", 0, 1, 21, 12);
    public bool IsPlayerTurn { get; private set; } = true;
    public bool IsOver { get; private set; } = false;
    public int initialExp;
    private BasicCommand selectedCommand = BasicCommand.attack;

    void Awake()
    {
        enemy.ShowAllStatus();
        player.ShowAllStatus();
    }

    void Update()
    {
        if (IsOver)
        {
            if (player.Hp > 0)
            {
                ShowResult();
            }
        }
        else
        {
            if (enemy.Hp <= 0)
            {
                IsOver = true;
            }

            GameObject turnIcon = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Basic/Turn");
            GameObject commandWindow = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Commands");

            if (player.Hp <= 0)
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
                        enemy.Attack(player);
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
        dungeonUi.SetActive(false);
        battleUi.SetActive(true);
        initialExp = player.Exp;
    }

    void OnDisable()
    {
        GetComponent<CameraController>().enabled = true;
        if (dungeonUi && battleUi)
        {
            dungeonUi.SetActive(true);
            battleUi.SetActive(false);
        }
    }

    public void SelectBasicCommand()
    {
        GameObject attackArrow = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Commands/Attack/Arrow");
        GameObject skillsArrow = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Commands/Skills/Arrow");
        GameObject itemsArrow = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Commands/Items/Arrow");

        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedCommand = selectedCommand == BasicCommand.attack ? BasicCommand.items : selectedCommand - 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            selectedCommand = selectedCommand == BasicCommand.items ? BasicCommand.attack : selectedCommand + 1;
        }

        attackArrow.SetActive(selectedCommand == BasicCommand.attack);
        skillsArrow.SetActive(selectedCommand == BasicCommand.skills);
        itemsArrow.SetActive(selectedCommand == BasicCommand.items);
    }

    public void ExecuteBasicCommand()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (selectedCommand)
            {
                case BasicCommand.attack:
                    player.Attack(enemy);
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
        enemy.HideImage();

        if (initialExp == player.Exp)
        {
            player.Exp += enemy.DropExp;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            enemy.Hp = enemy.MaxHp;
            enemy.ShowImage();
            enemy.ResetHpBar();
            IsPlayerTurn = true;
            IsOver = false;
            enabled = false;
        }
    }
}