using NUnit.Framework;

public class EnemyDataTest
{
    [Test]
    public void EnemyDataTest_Initialize()
    {
        var enemyData = new EnemyData("Test", 1, 100, 10, 11, 12, 13, 14, 15, 20);

        Assert.That(enemyData, Is.Not.Null);
        Assert.That(enemyData, Is.InstanceOf<CharacterData>());
        Assert.That(enemyData, Is.InstanceOf<EnemyData>());

        Assert.That(enemyData.Name, Is.EqualTo("Test"));
        Assert.That(enemyData.Level, Is.EqualTo(1));
        Assert.That(enemyData.Hp, Is.EqualTo(100));
        Assert.That(enemyData.MaxHp, Is.EqualTo(100));
        Assert.That(enemyData.Atk, Is.EqualTo(10));
        Assert.That(enemyData.Mag, Is.EqualTo(11));
        Assert.That(enemyData.Def, Is.EqualTo(12));
        Assert.That(enemyData.Res, Is.EqualTo(13));
        Assert.That(enemyData.Agi, Is.EqualTo(14));
        Assert.That(enemyData.Luk, Is.EqualTo(15));
        Assert.That(enemyData.DropExp, Is.EqualTo(20));

        Assert.That(enemyData.IsAlive, Is.True);
    }
}
