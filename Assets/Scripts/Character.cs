public abstract class Character
{
    public string Name { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    abstract public int HpBarWidth { get; }

    public Character(string name, int hp)
    {
        Name = name;
        Hp = hp;
        MaxHp = hp;
    }

    public abstract void Attack(Character character);
    public abstract void RenderHpBar();
    public abstract void ResetHpBar();
}