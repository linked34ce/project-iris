public abstract class Character : ICharacter
{
    protected string _name;
    protected int _level;

    public Character(string name, int level)
    {
        _name = name;
        _level = level;
    }

    protected abstract void ShowAllStatus();
    public abstract void Initialize();
    public abstract void TakeDamage(int damage);
    public abstract void Attack(ICharacter character, int damage);
}
