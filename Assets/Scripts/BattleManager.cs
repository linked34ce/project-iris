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
            else if (IsPlayerTurn)
            {
                GameObject attackArrow = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Commands/Attack/Arrow");
                GameObject skillsArrow = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Commands/Skills/Arrow");
                GameObject itemsArrow = GameObject.Find("/Battle UI/Panel/Status/Attacker 1/Commands/Items/Arrow");

                if (!turnIcon.activeSelf)
                {
                    turnIcon.SetActive(true);
                    commandWindow.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    switch (selectedCommand)
                    {
                        case BasicCommand.attack:
                            selectedCommand = BasicCommand.items;
                            break;
                        case BasicCommand.skills:
                            selectedCommand = BasicCommand.attack;
                            break;
                        case BasicCommand.items:
                            selectedCommand = BasicCommand.skills;
                            break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    switch (selectedCommand)
                    {
                        case BasicCommand.attack:
                            selectedCommand = BasicCommand.skills;
                            break;
                        case BasicCommand.skills:
                            selectedCommand = BasicCommand.items;
                            break;
                        case BasicCommand.items:
                            selectedCommand = BasicCommand.attack;
                            break;
                    }
                }

                if (selectedCommand == BasicCommand.attack)
                {
                    if (!attackArrow.activeSelf)
                    {
                        attackArrow.SetActive(true);
                        skillsArrow.SetActive(false);
                        itemsArrow.SetActive(false);
                    }

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        player.Attack(enemy);
                        IsPlayerTurn = !IsPlayerTurn;
                    }
                }
                else if (selectedCommand == BasicCommand.skills)
                {
                    if (!skillsArrow.activeSelf)
                    {
                        attackArrow.SetActive(false);
                        skillsArrow.SetActive(true);
                        itemsArrow.SetActive(false);
                    }

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                    }
                }
                else if (selectedCommand == BasicCommand.items)
                {
                    if (!itemsArrow.activeSelf)
                    {
                        attackArrow.SetActive(false);
                        skillsArrow.SetActive(false);
                        itemsArrow.SetActive(true);
                    }

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                    }
                }
            }
            else
            {
                if (turnIcon.activeSelf)
                {
                    turnIcon.SetActive(false);
                    commandWindow.SetActive(false);
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    enemy.Attack(player);
                    IsPlayerTurn = !IsPlayerTurn;
                }
            }
        }
    }

    void OnEnable()
    {
        GetComponent<FPSController>().enabled = false;
        dungeonUi.SetActive(false);
        battleUi.SetActive(true);
        initialExp = player.Exp;
    }

    void OnDisable()
    {
        GetComponent<FPSController>().enabled = true;
        if (dungeonUi && battleUi)
        {
            dungeonUi.SetActive(true);
            battleUi.SetActive(false);
        }
    }
}