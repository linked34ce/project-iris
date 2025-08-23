using UnityEngine;

public abstract class Character : MonoBehaviour, ICharacter
{
    [SerializeField] protected string _name;
    [SerializeField] protected int _level;

    protected abstract void ShowAllStatus();
    public abstract void Initialize();
    public abstract void TakeDamage(int damage);
    public abstract void Attack(ICharacter character, int damage);
}
