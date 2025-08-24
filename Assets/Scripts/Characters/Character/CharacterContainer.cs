using UnityEngine;

public abstract class CharacterContainer : MonoBehaviour, ICharacterContainer
{
    [SerializeField] protected string _name;
    [SerializeField] protected int _level;
    public abstract void Initialize();
}
