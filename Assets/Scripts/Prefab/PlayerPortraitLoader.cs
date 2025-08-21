using System.Threading.Tasks;

public class PlayerPortraitLoader : PrefabLoader<Task>
{
    public override void Initialize()
    {
        PrefabManager = new PrefabManager(_address, _transform);
    }

    public override async Task Create() => await PrefabManager.LoadPrefab();

    public override void Destroy() => PrefabManager.DestroyPrefab();
}
