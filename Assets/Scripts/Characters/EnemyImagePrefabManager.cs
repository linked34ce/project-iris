using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyImagePrefabManager : MonoBehaviour
{
    private AsyncOperationHandle<GameObject> _handle;
    private GameObject _prefab;

    public Animator EnemyImageAnimator { get; private set; }

    private static EnemyImagePrefabManager s_instance;
    public static EnemyImagePrefabManager Instance
    {
        get
        {
            if (null == s_instance)
            {
                s_instance = (EnemyImagePrefabManager)FindAnyObjectByType(
                    typeof(EnemyImagePrefabManager)
                );
                if (null == s_instance)
                {
                    Debug.Log("EnemyImagePrefabManager Instance Error");
                }
            }
            return s_instance;
        }
    }

    public async Task LoadImagePrefab(string address)
    {
        if (EnemyImageAnimator == null)
        {
            await Addressables.InitializeAsync().Task;
            _handle = Addressables.InstantiateAsync(address, transform);
            _prefab = _handle.WaitForCompletion();
            EnemyImageAnimator = _prefab.GetComponent<Animator>();
        }
    }

    public void DestroyImagePrefab()
    {
        Addressables.ReleaseInstance(_prefab);
        EnemyImageAnimator = null;
    }
}
