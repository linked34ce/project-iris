using UnityEngine;

public abstract class PrefabLoader<T> : MonoBehaviour, IPrefabLoader<T>
{
    public PrefabManager PrefabManager;
    [SerializeField] protected string _address;
    [SerializeField] protected Transform _transform;

    protected void Awake() => PrefabManager = new PrefabManager(_address, _transform);

    public abstract T Create();
    public abstract void Destroy();
}
