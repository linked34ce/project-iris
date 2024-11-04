using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject dungeonUi;
    [SerializeField] GameObject battleUi;
    private readonly Enemy enemy = new("ツチノコ", 2, 10);
    private readonly Player player = new("歩夢", 1, 21, 12);
    public bool IsPlayerTurn { get; set; } = true;

    void Awake()
    {
        enemy.ShowLevel();
        player.ShowAllStatus();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (IsPlayerTurn)
            {
                player.Attack(enemy);
            }
            else
            {
                enemy.Attack(player);
            }
            IsPlayerTurn = !IsPlayerTurn;
        }

        if (enemy.Hp <= 0)
        {
            enemy.Hp = 10;
            enemy.ResetHpBar();
            IsPlayerTurn = true;
            enabled = false;
        }

        if (player.Hp <= 0)
        {
            enemy.Hp = 10;
            player.Hp = 10;
            enemy.ResetHpBar();
            player.ResetHpBar();
            IsPlayerTurn = true;
            enabled = false;
            Initiate.Fade("Scenes/Menu/Game Over", Color.black, 0.4f);
        }
    }

    void OnEnable()
    {
        GetComponent<FPSController>().enabled = false;
        dungeonUi.SetActive(false);
        battleUi.SetActive(true);
    }

    void OnDisable()
    {
        GetComponent<FPSController>().enabled = true;
        dungeonUi.SetActive(true);
        battleUi.SetActive(false);
    }
}