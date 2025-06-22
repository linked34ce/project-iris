using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class PrefabManager<T>
    : SingletonMonoBehaviour<PrefabManager<T>> where T : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    private AsyncOperationHandle<GameObject> _handle;
    private GameObject _prefab;
    private Component _component;

    public async Task LoadPrefab(string address)
    {
        if (_prefab == null)
        {
            await Addressables.InitializeAsync().Task;
            _handle = Addressables.InstantiateAsync(address, _transform);
            _prefab = _handle.WaitForCompletion();
        }
    }

    public void DestroyPrefab()
    {
        Addressables.ReleaseInstance(_prefab);
        _prefab = null;
        _component = null;
    }

    public TComponent GetComponentFromPrefab<TComponent>() where TComponent : Component
    {
        if (_prefab == null)
        {
            Debug.LogError("Prefab must be loaded before getting its component.");
            return default;
        }

        if (_component == null)
        {
            _component = _prefab.GetComponent<TComponent>();
        }

        return _component as TComponent;
    }
}
