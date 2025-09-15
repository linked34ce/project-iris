using NUnit.Framework;
using Moq;

using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class EnemyTest
{
    [Test]
    [Category("Enemy")]
    public void Instantiate()
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

        Assert.That(enemy, Is.Not.Null);
        Assert.That(enemy, Is.InstanceOf<Character>());
        Assert.That(enemy, Is.InstanceOf<Enemy>());

        Assert.That(enemy.Data.Name, Is.EqualTo("Test"));
        Assert.That(enemy.Data.Level, Is.EqualTo(1));

        Assert.That(enemy.Data.Hp, Is.EqualTo(100));
        Assert.That(enemy.Data.MaxHp, Is.EqualTo(100));
        Assert.That(enemy.Data.Atk, Is.EqualTo(10));
        Assert.That(enemy.Data.Mag, Is.EqualTo(11));
        Assert.That(enemy.Data.Def, Is.EqualTo(12));
        Assert.That(enemy.Data.Res, Is.EqualTo(13));
        Assert.That(enemy.Data.Agi, Is.EqualTo(14));
        Assert.That(enemy.Data.Luk, Is.EqualTo(15));
        Assert.That(enemy.Data.DropExp, Is.EqualTo(20));

        Assert.That(enemy.Data.IsAlive, Is.True);
    }

    [Test]
    [Category("Enemy")]
    public void Initialize()
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

        enemy.Initialize();

        enemyViewMock.Verify(x => x.ShowName(enemy.Data.Name), Times.Once);
        enemyViewMock.Verify(x => x.ShowLevel(enemy.Data.Level), Times.Once);
        enemyViewMock.Verify(x => x.ShowHp(enemy.Data.Hp, enemy.Data.MaxHp), Times.Once);
        enemyViewMock.Verify(x => x.ShowImage(), Times.Once);
    }

    [Test]
    [Category("Enemy")]
    public void TakeDamage()
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

        enemy.TakeDamage(15);

        Assert.That(enemy.Data.Hp, Is.EqualTo(85));
        enemyViewMock.Verify(x => x.ShowHp(enemy.Data.Hp, enemy.Data.MaxHp), Times.Once);
    }

    [Test]
    [Category("Enemy")]
    public void Attack()
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

        enemy.Attack(playerMock.Object, 5);

        playerMock.Verify(x => x.TakeDamage(5), Times.Once);
    }

    [Test]
    [Category("Enemy")]
    public void Attack_Exception()
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

        var characterMock = new Mock<ICharacter>();

        enemy.Attack(characterMock.Object, 5);

        characterMock.Verify(x => x.TakeDamage(5), Times.Never);
        loggerMock.Verify(x => x.Error("Target is not Player."), Times.Once);
    }

    [Test]
    [Category("Enemy")]
    public void OnAttacked()
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

        enemy.OnAttacked();

        enemyViewMock.Verify(x => x.PlayOnAttackedAnimation(), Times.Once);
    }
}
