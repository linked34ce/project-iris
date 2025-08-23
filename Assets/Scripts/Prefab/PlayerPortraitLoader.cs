using System.Threading.Tasks;

public class PlayerPortraitLoader : PrefabLoader<Task>
{
    protected override void Awake() => PrefabManager = new PrefabManager(_address, _transform);

    public override async Task Create() => await PrefabManager.LoadPrefab();

    public override void Destroy() => PrefabManager.DestroyPrefab();
}
