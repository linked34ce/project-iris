using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject dungeonUi;
    [SerializeField] GameObject battleUi;
    private readonly Enemy enemy = new(10);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Battle();
        }

        if (enemy.Hp <= 0)
        {
            enabled = false;
            enemy.Hp = 10;
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

    public void Battle()
    {
        enemy.Hp -= 5;
        Debug.Log("Enemy's HP: " + enemy.Hp);
    }
}