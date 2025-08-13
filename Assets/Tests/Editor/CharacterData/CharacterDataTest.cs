using NUnit.Framework;

public class CharcterDataTest
{
    private class TestCharacterData : CharacterData
    {
        public TestCharacterData(
            string name,
            int level,
            int hp,
            int atk,
            int mag,
            int def,
            int res,
            int agi,
            int luk
        ) : base(name, level, hp, atk, mag, def, res, agi, luk) { }
    }

    [Test]
    public void CharacterDataTest_Initialize()
    {
        var characterData = new TestCharacterData("Test", 1, 100, 10, 11, 12, 13, 14, 15);

        Assert.That(characterData, Is.Not.Null);
        Assert.That(characterData, Is.InstanceOf<CharacterData>());
        Assert.That(characterData.Name, Is.EqualTo("Test"));
        Assert.That(characterData.Level, Is.EqualTo(1));
        Assert.That(characterData.Hp, Is.EqualTo(100));
        Assert.That(characterData.MaxHp, Is.EqualTo(100));
        Assert.That(characterData.Atk, Is.EqualTo(10));
        Assert.That(characterData.Mag, Is.EqualTo(11));
        Assert.That(characterData.Def, Is.EqualTo(12));
        Assert.That(characterData.Res, Is.EqualTo(13));
        Assert.That(characterData.Agi, Is.EqualTo(14));
        Assert.That(characterData.Luk, Is.EqualTo(15));
        Assert.That(characterData.IsAlive, Is.True);
    }

    [Test]
    public void CharacterDataTest_ClampHp()
    {
        var characterData = new TestCharacterData("Test", 1, 100, 10, 11, 12, 13, 14, 15)
        {
            Hp = 150
        };
        Assert.That(characterData.Hp, Is.EqualTo(100));

        characterData.Hp = -50;
        Assert.That(characterData.Hp, Is.EqualTo(0));
    }

    [Test]
    public void CharacterDataTest_Alive()
    {
        var characterData = new TestCharacterData("Test", 1, 100, 10, 11, 12, 13, 14, 15)
        {
            IsAlive = false
        };
        Assert.That(characterData.Hp, Is.EqualTo(0));
    }

    [Test]
    public void CharacterDataTest_TakeDamage()
    {
        var characterData = new TestCharacterData("Test", 1, 100, 10, 11, 12, 13, 14, 15);
        characterData.TakeDamage(30);
        Assert.That(characterData.Hp, Is.EqualTo(70));
    }
}
