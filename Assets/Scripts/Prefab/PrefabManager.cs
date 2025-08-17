using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PrefabManager
{
    private AsyncOperationHandle<GameObject> _handle;
    private GameObject _prefab;
    private Component _component;
    private readonly string _address;
    private readonly Transform _transform;

    public PrefabManager(string address, Transform transform)
    {
        _address = address;
        _transform = transform;
    }

    public async Task LoadPrefab()
    {
        if (_prefab == null)
        {
            await Addressables.InitializeAsync().Task;
            _handle = Addressables.InstantiateAsync(_address, _transform);
            _prefab = _handle.WaitForCompletion();
        }
    }

    public void DestroyPrefab()
    {
        Addressables.ReleaseInstance(_prefab);
        _prefab = null;
        _component = null;
    }

    public T GetComponentFromPrefab<T>() where T : Component
    {
        if (_prefab == null)
        {
            Debug.LogError("Prefab must be loaded before getting its component.");
            return default;
        }

        if (_component == null)
        {
            _component = _prefab.GetComponent<T>();
        }

        return _component as T;
    }
}
