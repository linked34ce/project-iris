public abstract class Character
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Hp { get; set; }
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
    public abstract void ShowLevel();
    public abstract void RenderHpBar();
    public abstract void ResetHpBar();
}