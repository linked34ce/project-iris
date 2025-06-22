using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int _hp;
    [SerializeField] private int _atk;
    [SerializeField] private int _mag;
    [SerializeField] private int _def;
    [SerializeField] private int _res;
    [SerializeField] private int _agi;
    [SerializeField] private int _luk;
    [SerializeField] private int _dropExp;

    [SerializeField] private Animator _imageAnimator;
    [SerializeField] private EnemyView _view;

    public EnemyData Data { get; protected set; }

    public bool IsAttacked
    {
        get => _imageAnimator.GetBool("isAttacked");
        set => _imageAnimator.SetBool("isAttacked", value);
    }

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

    public override void ShowAllStatus()
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

    public override void Attack(Character target, int damage)
    {
        if (target is Player player)
        {
            player.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("Target is not Player.");
        }
    }
}
