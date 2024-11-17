using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Status")]
public class EnemyData : Data
{
    [SerializeField] private int level;
    public int Level
    {
        get => level;
        set => level = value;
    }

    [SerializeField] private int maxHp;
    public int MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }

    [SerializeField] private int hp;
    public int Hp
    {
        get => hp;
        set => hp = Mathf.Clamp(value, 0, MaxHp);
    }

    [SerializeField] private int atk;
    public int Atk
    {
        get => atk;
        set => atk = value;
    }

    [SerializeField] private int mag;
    public int Mag
    {
        get => mag;
        set => mag = value;
    }

    [SerializeField] private int def;
    public int Def
    {
        get => def;
        set => def = value;
    }

    [SerializeField] private int res;
    public int Res
    {
        get => res;
        set => res = value;
    }

    [SerializeField] private int agi;
    public int Agi
    {
        get => agi;
        set => agi = value;
    }

    [SerializeField] private int luk;
    public int Luk
    {
        get => luk;
        set => luk = value;
    }
}