public abstract class Character
{
    public string Name { get; private set; }
    public int Level { get; set; }
    private int _hp;
    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value >= 0 ? value : 0;
        }
    }
    public int MaxHp { get; set; }
    abstract public int HpBarWidth { get; }

    public Character(string name, int level, int hp)
    {
        Name = name;
        Level = level;
        Hp = hp;
        MaxHp = hp;
    }

    public abstract void Attack(Character character);
    public abstract void ShowAllStatus();
    public abstract void ShowName();
    public abstract void ShowLevel();
    public abstract void RenderHpBar();
    public abstract void ResetHpBar();
}