using System.Collections;

using NUnit.Framework;
using Moq;

using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class BattleFlowControllerTest
{
    private class TestBattleFlowController : BattleFlowController
    {
        public bool IsOnPlayerTurnBeginCalled { get; private set; } = false;
        public bool IsOnEnemyTurnBeginCalled { get; private set; } = false;

        public TestBattleFlowController(
            IPlayer player,
            IEnemy enemy,
            ICoroutineController enemyCoroutineController,
            ICoroutineController playerCoroutineController,
            ISceneLoader sceneLoader
        ) : base(
            player,
            enemy,
            enemyCoroutineController,
            playerCoroutineController,
            sceneLoader
        )
        {
            OnPlayerTurnBegin = () => IsOnPlayerTurnBeginCalled = true;
            OnEnemyTurnBegin = () => IsOnEnemyTurnBeginCalled = true;
        }

        public void ResetAllMocks()
        {
            IsOnPlayerTurnBeginCalled = false;
            IsOnEnemyTurnBeginCalled = false;
        }

        public void CallOnEnemyTurn()
        {
            var enumerator = OnEnemyTurn();
            enumerator.MoveNext();
            enumerator.MoveNext();
        }

        public void CallOnPlayerTurn()
        {
            var enumerator = OnPlayerTurn();
            enumerator.MoveNext();
            enumerator.MoveNext();
        }
    }

    [Test]
    [Category("BattleFlowController")]
    public void Instantiate()
    {
        var playerMock = new Mock<IPlayer>();
        var enemyMock = new Mock<IEnemy>();
        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new BattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        Assert.That(flowController, Is.Not.Null);
        Assert.That(flowController, Is.InstanceOf<BattleFlowController>());

        Assert.That(flowController.Turn, Is.EqualTo(Turn.None));
        Assert.That(flowController.BattleState, Is.EqualTo(BattleState.None));
        Assert.That(flowController.OnPlayerTurnBegin, Is.Null);
        Assert.That(flowController.OnEnemyTurnBegin, Is.Null);
    }

    [Test]
    [Category("BattleFlowController")]
    public void BattleState_SetValueExceptGameOver()
    {
        var playerMock = new Mock<IPlayer>();
        var enemyMock = new Mock<IEnemy>();
        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new BattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        )
        {
            BattleState = BattleState.InBattle
        };

        sceneLoaderMock.Verify(x => x.LoadScene("Scenes/Menu/GameOver"), Times.Never);
    }

    [Test]
    [Category("BattleFlowController")]
    public void BattleState_SetGameOver()
    {
        var playerMock = new Mock<IPlayer>();
        var enemyMock = new Mock<IEnemy>();
        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new BattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        )
        {
            BattleState = BattleState.GameOver
        };

        sceneLoaderMock.Verify(x => x.LoadScene("Scenes/Menu/GameOver"), Times.Once);
    }

    [Test]
    [Category("BattleFlowController")]
    public void BattleState_SetSameValue()
    {
        var playerMock = new Mock<IPlayer>();
        var enemyMock = new Mock<IEnemy>();
        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new BattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        )
        {
            BattleState = BattleState.GameOver
        };

        sceneLoaderMock.Reset();

        flowController.BattleState = BattleState.GameOver;

        sceneLoaderMock.Verify(x => x.LoadScene("Scenes/Menu/GameOver"), Times.Never);
    }

    [Test]
    [Category("BattleFlowController")]
    public void Dispose()
    {
        var playerMock = new Mock<IPlayer>();
        var enemyMock = new Mock<IEnemy>();
        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new TestBattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        flowController.InitializeBattleState();
        flowController.ResetAllMocks();

        flowController.Dispose();

        enemyTurnCoroutineControllerMock.Verify(x => x.Stop(), Times.Once);

        Assert.That(flowController.Turn, Is.EqualTo(Turn.None));
        Assert.That(flowController.BattleState, Is.EqualTo(BattleState.None));
        Assert.That(flowController.OnPlayerTurnBegin, Is.Null);
        Assert.That(flowController.OnEnemyTurnBegin, Is.Null);
        Assert.That(flowController.IsOnPlayerTurnBeginCalled, Is.False);
        Assert.That(flowController.IsOnEnemyTurnBeginCalled, Is.False);
    }

    [Test]
    [Category("BattleFlowController")]
    public void InitializeBattleState()
    {
        var playerMock = new Mock<IPlayer>();
        var enemyMock = new Mock<IEnemy>();
        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new TestBattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        flowController.InitializeBattleState();

        Assert.That(flowController.Turn, Is.EqualTo(Turn.Player));
        Assert.That(flowController.IsOnPlayerTurnBeginCalled, Is.True);
        Assert.That(flowController.IsOnEnemyTurnBeginCalled, Is.False);
        Assert.That(flowController.BattleState, Is.EqualTo(BattleState.InBattle));
    }

    [Test]
    [Category("BattleFlowController")]
    public void PlayerAttack_CallOnce()
    {
        var playerMock = new Mock<IPlayer>();
        var enemyMock = new Mock<IEnemy>();
        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new TestBattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        flowController.PlayerAttack(5);

        playerMock.Verify(x => x.Attack(enemyMock.Object, 5), Times.Once);
        Assert.That(flowController.Turn, Is.EqualTo(Turn.Enemy));
        Assert.That(flowController.IsOnPlayerTurnBeginCalled, Is.False);
        Assert.That(flowController.IsOnEnemyTurnBeginCalled, Is.True);
        enemyTurnCoroutineControllerMock.Verify(
            x => x.Begin(It.IsAny<IEnumerator>()),
            Times.Once
        );
    }

    [Test]
    [Category("BattleFlowController")]
    public void PlayerAttack_CallTwice()
    {
        var playerMock = new Mock<IPlayer>();
        var enemyMock = new Mock<IEnemy>();
        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new TestBattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        flowController.PlayerAttack(5);
        flowController.ResetAllMocks();

        flowController.PlayerAttack(5);

        playerMock.Verify(x => x.Attack(enemyMock.Object, 5), Times.Exactly(2));
        Assert.That(flowController.Turn, Is.EqualTo(Turn.Enemy));
        Assert.That(flowController.IsOnPlayerTurnBeginCalled, Is.False);
        Assert.That(flowController.IsOnEnemyTurnBeginCalled, Is.False);
        enemyTurnCoroutineControllerMock.Verify(
            x => x.Begin(It.IsAny<IEnumerator>()),
            Times.Exactly(2)
        );
    }

    [Test]
    [Category("BattleFlowController")]
    public void OnEnemyTurn_BothAreAlive()
    {
        var playerMock = new Mock<IPlayer>();
        var testPlayerData = new PlayerData("Test", 1, "healer");
        playerMock.SetupGet(x => x.Data).Returns(testPlayerData);

        var enemyMock = new Mock<IEnemy>();
        var testEnemyData = new EnemyData("Test", 1, 100, 10, 11, 12, 13, 14, 15, 20);
        enemyMock.SetupGet(x => x.Data).Returns(testEnemyData);

        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new TestBattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        flowController.CallOnEnemyTurn();

        enemyMock.Verify(x => x.Attack(playerMock.Object, 5), Times.Once);
        playerTurnCoroutineControllerMock.Verify(
            x => x.Begin(It.IsAny<IEnumerator>()),
            Times.Once
        );
        Assert.That(flowController.BattleState, Is.EqualTo(BattleState.InBattle));
        enemyTurnCoroutineControllerMock.Verify(x => x.Stop(), Times.Once);
    }

    [Test]
    [Category("BattleFlowController")]
    public void OnEnemyTurn_PlayerIsNotAlive()
    {
        var playerMock = new Mock<IPlayer>();
        var testPlayerData = new PlayerData("Test", 1, "healer")
        {
            IsAlive = false
        };
        playerMock.SetupGet(x => x.Data).Returns(testPlayerData);

        var enemyMock = new Mock<IEnemy>();
        var testEnemyData = new EnemyData("Test", 1, 100, 10, 11, 12, 13, 14, 15, 20);
        enemyMock.SetupGet(x => x.Data).Returns(testEnemyData);

        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new TestBattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        flowController.CallOnEnemyTurn();

        enemyMock.Verify(x => x.Attack(playerMock.Object, 5), Times.Once);
        playerTurnCoroutineControllerMock.Verify(
            x => x.Begin(It.IsAny<IEnumerator>()),
            Times.Never
        );
        Assert.That(flowController.BattleState, Is.EqualTo(BattleState.GameOver));
        enemyTurnCoroutineControllerMock.Verify(x => x.Stop(), Times.Once);
    }

    [Test]
    [Category("BattleFlowController")]
    public void OnEnemyTurn_EnemyIsNotAlive()
    {
        var playerMock = new Mock<IPlayer>();
        var testPlayerData = new PlayerData("Test", 1, "healer");

        playerMock.SetupGet(x => x.Data).Returns(testPlayerData);

        var enemyMock = new Mock<IEnemy>();
        var testEnemyData = new EnemyData("Test", 1, 100, 10, 11, 12, 13, 14, 15, 20)
        {
            IsAlive = false
        };
        enemyMock.SetupGet(x => x.Data).Returns(testEnemyData);

        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new TestBattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        flowController.CallOnEnemyTurn();

        enemyMock.Verify(x => x.Attack(playerMock.Object, 5), Times.Never);
        playerTurnCoroutineControllerMock.Verify(
            x => x.Begin(It.IsAny<IEnumerator>()),
            Times.Never
        );
        Assert.That(flowController.BattleState, Is.EqualTo(BattleState.Victory));
        enemyTurnCoroutineControllerMock.Verify(x => x.Stop(), Times.Once);
    }

    [Test]
    [Category("BattleFlowController")]
    public void OnEnemyTurn_BothAreNotAlive()
    {
        var playerMock = new Mock<IPlayer>();
        var testPlayerData = new PlayerData("Test", 1, "healer")
        {
            IsAlive = false
        };

        playerMock.SetupGet(x => x.Data).Returns(testPlayerData);

        var enemyMock = new Mock<IEnemy>();
        var testEnemyData = new EnemyData("Test", 1, 100, 10, 11, 12, 13, 14, 15, 20)
        {
            IsAlive = false
        };
        enemyMock.SetupGet(x => x.Data).Returns(testEnemyData);

        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new TestBattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        flowController.CallOnEnemyTurn();

        enemyMock.Verify(x => x.Attack(playerMock.Object, 5), Times.Never);
        playerTurnCoroutineControllerMock.Verify(
            x => x.Begin(It.IsAny<IEnumerator>()),
            Times.Never
        );
        Assert.That(flowController.BattleState, Is.EqualTo(BattleState.GameOver));
        enemyTurnCoroutineControllerMock.Verify(x => x.Stop(), Times.Once);
    }

    [Test]
    [Category("BattleFlowController")]
    public void OnPlayerTurn()
    {
        var playerMock = new Mock<IPlayer>();
        var testPlayerData = new PlayerData("Test", 1, "healer");
        playerMock.SetupGet(x => x.Data).Returns(testPlayerData);

        var enemyMock = new Mock<IEnemy>();
        var testEnemyData = new EnemyData("Test", 1, 100, 10, 11, 12, 13, 14, 15, 20);
        enemyMock.SetupGet(x => x.Data).Returns(testEnemyData);

        var enemyTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var playerTurnCoroutineControllerMock = new Mock<ICoroutineController>();
        var sceneLoaderMock = new Mock<ISceneLoader>();

        var flowController = new TestBattleFlowController(
            playerMock.Object,
            enemyMock.Object,
            enemyTurnCoroutineControllerMock.Object,
            playerTurnCoroutineControllerMock.Object,
            sceneLoaderMock.Object
        );

        flowController.CallOnPlayerTurn();

        Assert.That(flowController.Turn, Is.EqualTo(Turn.Player));
        Assert.That(flowController.BattleState, Is.EqualTo(BattleState.InBattle));
        playerTurnCoroutineControllerMock.Verify(x => x.Stop(), Times.Once);
    }
}
