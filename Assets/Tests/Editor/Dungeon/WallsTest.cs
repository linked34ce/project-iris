using NUnit.Framework;

using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class WallsTest
{
    [Test]
    [Category("Walls")]
    public void Instantiate_WithInt()
    {
        var walls = new Walls(0, 1, 2, 3);

        Assert.That(walls, Is.Not.Null);
        Assert.That(walls, Is.InstanceOf<Walls>());

        Assert.That(walls.East, Is.EqualTo(Wall.air));
        Assert.That(walls.South, Is.EqualTo(Wall.wall));
        Assert.That(walls.West, Is.EqualTo(Wall.stairs));
        Assert.That(walls.North, Is.EqualTo(Wall.undefined));
    }

    [Test]
    [Category("Walls")]
    public void Instantiate_WithEnum()
    {
        var walls = new Walls(Wall.air, Wall.wall, Wall.stairs, Wall.undefined);

        Assert.That(walls, Is.Not.Null);
        Assert.That(walls, Is.InstanceOf<Walls>());

        Assert.That(walls.East, Is.EqualTo(Wall.air));
        Assert.That(walls.South, Is.EqualTo(Wall.wall));
        Assert.That(walls.West, Is.EqualTo(Wall.stairs));
        Assert.That(walls.North, Is.EqualTo(Wall.undefined));
    }
}
