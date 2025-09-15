using NUnit.Framework;
using Moq;

public class PlayerTest
{
    [Test]
    [Category("Player")]
    public void Instantiate()
    {
        var playerViewMock = new Mock<IPlayerView>();
        var soundProviderMock = new Mock<IBattleSoundProvider>();
        var loggerMock = new Mock<IUnityLogger>();

        var player = new Player(
            "Test",
            1,
            "healer",
            playerViewMock.Object,
            soundProviderMock.Object,
            loggerMock.Object
        );

        var testPlayerData = new PlayerData("Test", 1, "healer");

        Assert.That(player, Is.Not.Null);
        Assert.That(player, Is.InstanceOf<Character>());
        Assert.That(player, Is.InstanceOf<Player>());

        Assert.That(player.Data.Name, Is.EqualTo(testPlayerData.Name));
        Assert.That(player.Data.Level, Is.EqualTo(testPlayerData.Level));
        Assert.That(player.Data.Role, Is.EqualTo(testPlayerData.Role));

        Assert.That(player.Data.Hp, Is.EqualTo(testPlayerData.Hp));
        Assert.That(player.Data.MaxHp, Is.EqualTo(testPlayerData.MaxHp));
        Assert.That(player.Data.Sp, Is.EqualTo(testPlayerData.Sp));
        Assert.That(player.Data.MaxSp, Is.EqualTo(testPlayerData.MaxSp));
        Assert.That(player.Data.Atk, Is.EqualTo(testPlayerData.Atk));
        Assert.That(player.Data.Mag, Is.EqualTo(testPlayerData.Mag));
        Assert.That(player.Data.Def, Is.EqualTo(testPlayerData.Def));
        Assert.That(player.Data.Res, Is.EqualTo(testPlayerData.Res));
        Assert.That(player.Data.Agi, Is.EqualTo(testPlayerData.Agi));
        Assert.That(player.Data.Luk, Is.EqualTo(testPlayerData.Luk));
        Assert.That(player.Data.Exp, Is.EqualTo(testPlayerData.Exp));
        Assert.That(player.Data.NextExp, Is.EqualTo(testPlayerData.NextExp));

        Assert.That(player.Data.IsAlive, Is.EqualTo(testPlayerData.IsAlive));
        Assert.That(player.Data.HasLeveledUp, Is.EqualTo(testPlayerData.HasLeveledUp));
    }

    [Test]
    [Category("Player")]
    public void Initialize()
    {
        var playerViewMock = new Mock<IPlayerView>();
        var soundProviderMock = new Mock<IBattleSoundProvider>();
        var loggerMock = new Mock<IUnityLogger>();

        var player = new Player(
            "Test",
            1,
            "healer",
            playerViewMock.Object,
            soundProviderMock.Object,
            loggerMock.Object
        );

        player.Initialize();

        playerViewMock.Verify(x => x.ShowName(player.Data.Name), Times.Once);
        playerViewMock.Verify(x => x.ShowLevel(player.Data.Level), Times.Once);
        playerViewMock.Verify(x => x.ShowHp(player.Data.Hp, player.Data.MaxHp), Times.Once);
        playerViewMock.Verify(x => x.ShowSp(player.Data.Sp, player.Data.MaxSp), Times.Once);
    }

    [Test]
    [Category("Player")]
    public void TakeDamage()
    {
        var playerViewMock = new Mock<IPlayerView>();
        var soundProviderMock = new Mock<IBattleSoundProvider>();
        var loggerMock = new Mock<IUnityLogger>();

        var player = new Player(
            "Test",
            1,
            "healer",
            playerViewMock.Object,
            soundProviderMock.Object,
            loggerMock.Object
        );
        player.Data.Hp = 20;

        player.TakeDamage(15);

        Assert.That(player.Data.Hp, Is.EqualTo(5));
        playerViewMock.Verify(x => x.ShowHp(player.Data.Hp, player.Data.MaxHp), Times.Once);
    }

    [Test]
    [Category("Player")]
    public void Attack()
    {
        var playerViewMock = new Mock<IPlayerView>();
        var soundProviderMock = new Mock<IBattleSoundProvider>();
        var loggerMock = new Mock<IUnityLogger>();

        var player = new Player(
            "Test",
            1,
            "healer",
            playerViewMock.Object,
            soundProviderMock.Object,
            loggerMock.Object
        );

        var enemyMock = new Mock<IEnemy>();

        player.Attack(enemyMock.Object, 5);

        enemyMock.Verify(x => x.OnAttacked(), Times.Once);
        soundProviderMock.Verify(x => x.PlayAttack(), Times.Once);
        enemyMock.Verify(x => x.TakeDamage(5), Times.Once);
    }

    [Test]
    [Category("Player")]
    public void Attack_Exception()
    {
        var playerViewMock = new Mock<IPlayerView>();
        var soundProviderMock = new Mock<IBattleSoundProvider>();
        var loggerMock = new Mock<IUnityLogger>();

        var player = new Player(
            "Test",
            1,
            "healer",
            playerViewMock.Object,
            soundProviderMock.Object,
            loggerMock.Object
        );

        var characterMock = new Mock<ICharacter>();

        player.Attack(characterMock.Object, 5);

        soundProviderMock.Verify(x => x.PlayAttack(), Times.Never);
        characterMock.Verify(x => x.TakeDamage(5), Times.Never);
        loggerMock.Verify(x => x.Error("Target is not Enemy."), Times.Once);
    }

    [Test]
    [Category("Player")]
    public void GainExp()
    {
        var playerViewMock = new Mock<IPlayerView>();
        var soundProviderMock = new Mock<IBattleSoundProvider>();
        var loggerMock = new Mock<IUnityLogger>();

        var player = new Player(
            "Test",
            1,
            "healer",
            playerViewMock.Object,
            soundProviderMock.Object,
            loggerMock.Object
        );

        var enemyMock = new Mock<IEnemy>();
        var testEnemyData = new EnemyData("Test", 1, 100, 10, 11, 12, 13, 14, 15, 20);
        enemyMock.SetupGet(x => x.Data).Returns(testEnemyData);

        player.GainExp(enemyMock.Object);

        Assert.That(player.Data.Exp, Is.EqualTo(20));
    }

    [Test]
    [Category("Player")]
    public void ShowResult()
    {
        var playerViewMock = new Mock<IPlayerView>();
        var soundProviderMock = new Mock<IBattleSoundProvider>();
        var loggerMock = new Mock<IUnityLogger>();

        var player = new Player(
            "Test",
            1,
            "healer",
            playerViewMock.Object,
            soundProviderMock.Object,
            loggerMock.Object
        );

        player.ShowResult();

        Assert.That(player.Data.Level, Is.EqualTo(1));
        playerViewMock.Verify(x => x.ShowLevel(player.Data.Level), Times.Never);
        playerViewMock.Verify(x => x.ShowHp(player.Data.Hp, player.Data.MaxHp), Times.Once);
        playerViewMock.Verify(x => x.ShowSp(player.Data.Sp, player.Data.MaxSp), Times.Once);
    }

    [Test]
    [Category("Player")]
    public void ShowResult_LevelUp()
    {
        var playerViewMock = new Mock<IPlayerView>();
        var soundProviderMock = new Mock<IBattleSoundProvider>();
        var loggerMock = new Mock<IUnityLogger>();

        var player = new Player(
            "Test",
            1,
            "healer",
            playerViewMock.Object,
            soundProviderMock.Object,
            loggerMock.Object
        );
        player.Data.Level = 2;
        player.Data.HasLeveledUp = true;

        player.ShowResult();

        Assert.That(player.Data.Level, Is.EqualTo(2));
        playerViewMock.Verify(x => x.ShowLevel(player.Data.Level), Times.Once);
        playerViewMock.Verify(x => x.ShowHp(player.Data.Hp, player.Data.MaxHp), Times.Once);
        playerViewMock.Verify(x => x.ShowSp(player.Data.Sp, player.Data.MaxSp), Times.Once);
    }
}
