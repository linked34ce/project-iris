using UnityEngine;

public class Player : Character, IPlayer
{
    // this property should be deleted when class for each role is made
    private readonly string _role;

    private readonly IPlayerView _view;
    private readonly IBattleSoundProvider _soundProvider;

    public PlayerData Data { get; protected set; }

    private readonly IUnityLogger _logger;

    public Player(
        string name,
        int level,
        string role,
        IPlayerView view,
        IBattleSoundProvider soundProvider,
        IUnityLogger logger
    ) : base(name, level)
    {
        _role = role;
        _view = view;
        _soundProvider = soundProvider;
        _logger = logger;
        Data = new PlayerData(_name, _level, _role);
    }

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
            enemy.OnAttacked();
            _soundProvider.PlayAttack();
            enemy.TakeDamage(damage);
        }
        else
        {
            _logger.Error("Target is not Enemy.");
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
