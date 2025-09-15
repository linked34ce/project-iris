using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public abstract class CharacterContainer : MonoBehaviour, ICharacterContainer
{
    [SerializeField] protected string _name;
    [SerializeField] protected int _level;
    public abstract void Initialize();
}
