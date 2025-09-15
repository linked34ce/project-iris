using System.Threading.Tasks;

using NUnit.Framework;
using Moq;

using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class BattleResultControllerTest
{
    [Test]
    [Category("BattleResultController")]
    public void Instantiate()
    {
        var playerMock = new Mock<IPlayer>();
        var enemyMock = new Mock<IEnemy>();
        var enemyLoaderMock = new Mock<IPrefabLoader<Task<IEnemy>>>();
        var battleResultMock = new Mock<IBattleResult>();

        var resultController = new BattleResultController(
            playerMock.Object,
            enemyMock.Object,
            enemyLoaderMock.Object,
            battleResultMock.Object
        );

        Assert.That(resultController, Is.Not.Null);
        Assert.That(resultController, Is.InstanceOf<BattleResultController>());

        Assert.That(resultController.BattleResult, Is.EqualTo(battleResultMock.Object));
    }

    [Test]
    [Category("BattleResultController")]
    public void ShowResult()
    {
        var enemyViewMock = new Mock<IEnemyView>();
        var loggerMock = new Mock<IUnityLogger>();

        var enemy = new Enemy(
            "Test",
            1,
            100,
            10,
            11,
            12,
            13,
            14,
            15,
            20,
            enemyViewMock.Object,
            loggerMock.Object
        );

        var playerMock = new Mock<IPlayer>();
        var enemyLoaderMock = new Mock<IPrefabLoader<Task<IEnemy>>>();
        var battleResultMock = new Mock<IBattleResult>();

        var resultController = new BattleResultController(
            playerMock.Object,
            enemy,
            enemyLoaderMock.Object,
            battleResultMock.Object
        );

        resultController.ShowResult();

        playerMock.Verify(x => x.GainExp(enemy), Times.Once());
        enemyLoaderMock.Verify(x => x.Destroy(), Times.Once());
        battleResultMock.Verify(x => x.Show(playerMock.Object), Times.Once());
    }
}
