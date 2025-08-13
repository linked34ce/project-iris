using NUnit.Framework;

public class PlayerDataTest
{
    [Test]
    public void PlayerDataTest_Initialize()
    {
        var playerData = new PlayerData("Test", 1);

        Assert.That(playerData, Is.Not.Null);
        Assert.That(playerData, Is.InstanceOf<CharacterData>());
        Assert.That(playerData, Is.InstanceOf<PlayerData>());

        Assert.That(playerData.Name, Is.EqualTo("Test"));
        Assert.That(playerData.Level, Is.EqualTo(1));

        Assert.That(playerData.Hp, Is.EqualTo(21));
        Assert.That(playerData.MaxHp, Is.EqualTo(21));
        Assert.That(playerData.Sp, Is.EqualTo(12));
        Assert.That(playerData.MaxSp, Is.EqualTo(12));
        Assert.That(playerData.Atk, Is.EqualTo(2));
        Assert.That(playerData.Mag, Is.EqualTo(5));
        Assert.That(playerData.Def, Is.EqualTo(2));
        Assert.That(playerData.Res, Is.EqualTo(6));
        Assert.That(playerData.Agi, Is.EqualTo(2));
        Assert.That(playerData.Luk, Is.EqualTo(4));
        Assert.That(playerData.Exp, Is.EqualTo(0));
        Assert.That(playerData.NextExp, Is.EqualTo(10));

        Assert.That(playerData.IsAlive, Is.True);
        Assert.That(playerData.HasLeveledUp, Is.False);
    }

    [Test]
    public void PlayerDataTest_LevelUp()
    {
        var playerData = new PlayerData("Test", 1)
        {
            Exp = 10
        };

        Assert.That(playerData.Level, Is.EqualTo(2));

        Assert.That(playerData.Hp, Is.EqualTo(22));
        Assert.That(playerData.MaxHp, Is.EqualTo(22));
        Assert.That(playerData.Sp, Is.EqualTo(13));
        Assert.That(playerData.MaxSp, Is.EqualTo(13));
        Assert.That(playerData.Atk, Is.EqualTo(2));
        Assert.That(playerData.Mag, Is.EqualTo(6));
        Assert.That(playerData.Def, Is.EqualTo(2));
        Assert.That(playerData.Res, Is.EqualTo(7));
        Assert.That(playerData.Agi, Is.EqualTo(2));
        Assert.That(playerData.Luk, Is.EqualTo(4));
        Assert.That(playerData.Exp, Is.EqualTo(10));
        Assert.That(playerData.NextExp, Is.EqualTo(20));

        Assert.That(playerData.HasLeveledUp, Is.True);
    }

    [Test]
    public void PlayerDataTest_LevelOverflow()
    {
        var playerData = new PlayerData("Test", 1)
        {
            Exp = 550
        };

        Assert.That(playerData.Level, Is.EqualTo(10));
        Assert.That(playerData.NextExp, Is.EqualTo(0));

        playerData.Exp = 1000;
        Assert.That(playerData.Level, Is.EqualTo(10));
        Assert.That(playerData.NextExp, Is.EqualTo(0));
    }
}
