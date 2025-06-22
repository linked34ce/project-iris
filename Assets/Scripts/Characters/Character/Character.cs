using UnityEngine;

public abstract class Character : MonoBehaviour, IInitializable, IStatusShowable, IFighter
{
    [SerializeField] protected string _name;
    [SerializeField] protected int _level;

    public abstract void ShowAllStatus();
    public abstract void Initialize();
    public abstract void TakeDamage(int damage);
    public abstract void Attack(Character character, int damage);
}
