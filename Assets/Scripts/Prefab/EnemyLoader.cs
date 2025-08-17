using System.Threading.Tasks;

public class EnemyLoader : PrefabLoader<Task<Enemy>>
{
    public override void Awake()
    {
        PrefabManager = new PrefabManager(_address, _transform);
    }

    public override async Task<Enemy> Create()
    {
        await PrefabManager.LoadPrefab();
        return PrefabManager.GetComponentFromPrefab<Enemy>();
    }

    public override void Destroy() => PrefabManager.DestroyPrefab();
}
