using UnityEngine;

public class Player : Character, IPlayer
{
    // this property should be deleted when class for each role is made
    [SerializeField] private string _role;

    [SerializeField] private int _exp;

    [SerializeField] private int _sp;

    [SerializeField] private PlayerView _view;
    [SerializeField] private BattleSoundProvider _soundProvider;

    public PlayerData Data { get; protected set; }

    void Awake() => Data = new PlayerData(_name, _level);

    public override void Initialize() => ShowAllStatus();

    public override void TakeDamage(int damage)
    {
        Data.TakeDamage(damage);
        _view.ShowHp(Data.Hp, Data.MaxHp);
    }

    protected override void ShowAllStatus()
    {
        _view.ShowName(Data.Name);
        _view.ShowLevel(Data.Level);
        _view.ShowHp(Data.Hp, Data.MaxHp);
        _view.ShowSp(Data.Sp, Data.MaxSp);
    }

    public override void Attack(ICharacter target, int damage)
    {
        if (target is IEnemy enemy)
        {
            enemy.IsAttacked = true;
            _soundProvider.PlayAttack();
            enemy.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("Target is not Enemy.");
        }
    }

    public void GainExp(IEnemy enemy)
    {
        Data.Exp += enemy.Data.DropExp;
    }

    public void ShowResult()
    {
        if (Data.HasLeveledUp)
        {
            _view.ShowLevel(Data.Level);
            Data.HasLeveledUp = false;
        }

        _view.ShowHp(Data.Hp, Data.MaxHp);
        _view.ShowSp(Data.Sp, Data.MaxSp);
    }
}
