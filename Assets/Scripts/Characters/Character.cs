using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private string _name;
    public string Name
    {
        get => _name;
        protected set => _name = value;
    }

    [SerializeField] private int _level;
    public int Level
    {
        get => _level;
        protected set => _level = value;
    }

    [SerializeField] private int _hp;
    public virtual int Hp
    {
        get => _hp;
        set
        {
            _hp = Mathf.Clamp(value, 0, MaxHp);
            ShowHp();
        }
    }

    public int MaxHp { get; protected set; }

    [SerializeField] private int _atk;
    public int Atk
    {
        get => _atk;
        protected set => _atk = value;
    }

    [SerializeField] private int _mag;
    public int Mag
    {
        get => _mag;
        protected set => _mag = value;
    }

    [SerializeField] private int _def;
    public int Def
    {
        get => _def;
        protected set => _def = value;
    }

    [SerializeField] private int _res;
    public int Res
    {
        get => _res;
        protected set => _res = value;
    }

    [SerializeField] private int _agi;
    public int Agi
    {
        get => _agi;
        protected set => _agi = value;
    }

    [SerializeField] private int _luk;
    public int Luk
    {
        get => _luk;
        protected set => _luk = value;
    }

    protected virtual void Awake()
    {
        MaxHp = Hp;
    }

    public abstract void Attack(Character character);
    public abstract void ShowName();
    public abstract void ShowLevel();
    public abstract void ShowHp();

    public virtual void ShowAllStatus()
    {
        ShowName();
        ShowLevel();
        ShowHp();
    }
}