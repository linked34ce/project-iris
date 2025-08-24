using System.Threading.Tasks;

public class EnemyLoader : PrefabLoader<Task<IEnemy>>
{
    public override async Task<IEnemy> Create()
    {
        await PrefabManager.LoadPrefab();
        var enemyContainer = PrefabManager.GetComponentFromPrefab<EnemyContainer>();
        enemyContainer.Initialize();
        return enemyContainer.Enemy;
    }

    public override void Destroy() => PrefabManager.DestroyPrefab();
}
