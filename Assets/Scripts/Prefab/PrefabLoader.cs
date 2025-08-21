using UnityEngine;

public abstract class PrefabLoader<T> : MonoBehaviour, IPrefabLoader<T>
{
    public PrefabManager PrefabManager;
    [SerializeField] protected string _address;
    [SerializeField] protected Transform _transform;

    public abstract void Initialize();

    public abstract T Create();

    public abstract void Destroy();
}
