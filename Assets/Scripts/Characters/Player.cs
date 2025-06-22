using UnityEngine;

public class Player : Character
{
    // this property should be deleted when class for each role is made
    [SerializeField] private string _role;

    [SerializeField] private int _exp;

    [SerializeField] private int _sp;

    [SerializeField] private PlayerView _view;

    public PlayerData Data { get; protected set; }

    public override void Initialize()
    {
        Data = new PlayerData(_name, _level);
        _view.ShowPortrait();
        ShowAllStatus();
    }

    public override void TakeDamage(int damage)
    {
        Data.TakeDamage(damage);
        _view.ShowHp(Data.Hp, Data.MaxHp);
    }

    public override void ShowAllStatus()
    {
        BattleResult.Instance.Hide();
        _view.ShowName(Data.Name);
        _view.ShowLevel(Data.Level);
        _view.ShowHp(Data.Hp, Data.MaxHp);
        _view.ShowSp(Data.Sp, Data.MaxSp);
    }

    public override void Attack(Character target, int damage)
    {
        if (target is Enemy enemy)
        {
            enemy.IsAttacked = true;
            BattleSounds.Instance.PlayAttack();
            enemy.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("Target is not Enemy.");
        }
    }

    public void LevelUp()
    {
        Data.LevelUp();
        _view.ShowLevel(Data.Level);
        _view.ShowLevelUp();
    }

    public void ShowResult()
    {
        _view.ShowHp(Data.Hp, Data.MaxHp);
        _view.ShowSp(Data.Sp, Data.MaxSp);
        BattleResult.Instance.Show(Data.NextExp);
    }
}
