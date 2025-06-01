using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AttackerPortraitPrefabManager : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    public Transform Transform => _transform;

    private AsyncOperationHandle<GameObject> _handle;

    private readonly Image _roleIcon;

    private static AttackerPortraitPrefabManager s_instance;
    public static AttackerPortraitPrefabManager Instance
    {
        get
        {
            if (null == s_instance)
            {
                s_instance = (AttackerPortraitPrefabManager)FindAnyObjectByType(
                    typeof(AttackerPortraitPrefabManager)
                );
                if (null == s_instance)
                {
                    Debug.Log("AttackerPortraitPrefabManager Instance Error");
                }
            }
            return s_instance;
        }
    }

    public async Task LoadPortraitPrefab(string address)
    {
        if (_roleIcon == null)
        {
            await Addressables.InitializeAsync().Task;
            _handle = Addressables.InstantiateAsync(address, Transform);

        }
    }

    public void DestroyPortraitPrefab() => Addressables.ReleaseInstance(_handle);
}
