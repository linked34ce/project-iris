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

    private readonly Animator _imageAnimator;
    private readonly IEnemyView _view;

    public EnemyData Data { get; protected set; }

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
        Animator imageAnimator,
        IEnemyView view
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
        _imageAnimator = imageAnimator;
        _view = view;
    }

    public void OnAttacked() => _imageAnimator.SetTrigger("Trigger");

    public override void Initialize()
    {
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
        ShowAllStatus();
    }

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
            Debug.LogError("Target is not Player.");
        }
    }
}
