using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Status")]
public class ScriptableEnemyData : Data
{
    [SerializeField] private int _level;
    public int Level
    {
        get => _level;
        set => _level = value;
    }

    [SerializeField] private int _maxHp;
    public int MaxHp
    {
        get => _maxHp;
        set => _maxHp = value;
    }

    [SerializeField] private int _hp;
    public int Hp
    {
        get => _hp;
        set => _hp = Mathf.Clamp(value, 0, MaxHp);
    }

    [SerializeField] private int _atk;
    public int Atk
    {
        get => _atk;
        set => _atk = value;
    }

    [SerializeField] private int _mag;
    public int Mag
    {
        get => _mag;
        set => _mag = value;
    }

    [SerializeField] private int _def;
    public int Def
    {
        get => _def;
        set => _def = value;
    }

    [SerializeField] private int _res;
    public int Res
    {
        get => _res;
        set => _res = value;
    }

    [SerializeField] private int _agi;
    public int Agi
    {
        get => _agi;
        set => _agi = value;
    }

    [SerializeField] private int _luk;
    public int Luk
    {
        get => _luk;
        set => _luk = value;
    }
}
