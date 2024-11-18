using UnityEngine;

public abstract class Character
{
    public string Name { get; private set; }
    public int Level { get; set; }

    private int _hp;
    public int Hp
    {
        get => _hp;
        set => _hp = Mathf.Clamp(value, 0, MaxHp);
    }

    public int MaxHp { get; set; }
    public int Atk { get; set; }
    public int Mag { get; set; }
    public int Def { get; set; }
    public int Res { get; set; }
    public int Agi { get; set; }
    public int Luk { get; set; }
    abstract public int HpBarWidth { get; }

    public Character(string name, int level)
    {
        Name = name;
        Level = level;
    }

    public Character(string name, int level, int hp, int atk, int mag, int def,
                    int res, int agi, int luk) : this(name, level)
    {
        MaxHp = hp;
        Hp = hp;
        Atk = atk;
        Mag = mag;
        Def = def;
        Res = res;
        Agi = agi;
        Luk = luk;
    }

    public abstract void Attack(Character character);
    public abstract void ShowAllStatus();
    public abstract void ShowName();
    public abstract void ShowLevel();
    public abstract void RenderHpBar();
    public abstract void ResetHpBar();
}