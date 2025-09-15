using UnityEngine;

public class Enemy : Character, IEnemy
{
    private readonly int _hp;
    private readonly int _atk;
    private readonly int _mag;
    private readonly int _def;
    private readonly int _res;
    private readonly int _agi;
    private readonly int _luk;
    private readonly int _dropExp;

    private readonly IEnemyView _view;

    public EnemyData Data { get; protected set; }

    private readonly IUnityLogger _logger;

    public Enemy(
        string name,
        int level,
        int hp,
        int atk,
        int mag,
        int def,
        int res,
        int agi,
        int luk,
        int dropExp,
        IEnemyView view,
        IUnityLogger logger
    ) : base(name, level)
    {
        _hp = hp;
        _atk = atk;
        _mag = mag;
        _def = def;
        _res = res;
        _agi = agi;
        _luk = luk;
        _dropExp = dropExp;
        _view = view;
        _logger = logger;
        Data = new EnemyData(
            _name,
            _level,
            _hp,
            _atk,
            _mag,
            _def,
            _res,
            _agi,
            _luk,
            _dropExp
        );
    }


    public override void Initialize() => ShowAllStatus();

    protected override void ShowAllStatus()
    {
        _view.ShowName(Data.Name);
        _view.ShowLevel(Data.Level);
        _view.ShowHp(Data.Hp, Data.MaxHp);
        _view.ShowImage();
    }

    public override void TakeDamage(int damage)
    {
        Data.TakeDamage(damage);
        _view.ShowHp(Data.Hp, Data.MaxHp);
    }

    public override void Attack(ICharacter target, int damage)
    {
        if (target is IPlayer player)
        {
            player.TakeDamage(damage);
        }
        else
        {
            _logger.Error("Target is not Player.");
        }
    }

    public void OnAttacked() => _view.PlayOnAttackedAnimation();
}
