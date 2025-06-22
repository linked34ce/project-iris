using UnityEngine;

public abstract class CharacterData : IBasicStatus, IBasicParameters, IDamageable
{
    public string Name { get; set; }
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

    public bool IsAlive
    {
        get => Hp > 0;
        set
        {
            if (!value)
            {
                Hp = 0;
            }
        }
    }

    public CharacterData(string name, int level)
    {
        Name = name;
        Level = level;
    }

    public CharacterData(
        string name,
        int level,
        int hp,
        int atk,
        int mag,
        int def,
        int res,
        int agi,
        int luk
    ) : this(name, level)
    {
        _hp = hp;
        MaxHp = hp;
        Atk = atk;
        Mag = mag;
        Def = def;
        Res = res;
        Agi = agi;
        Luk = luk;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
    }
}
