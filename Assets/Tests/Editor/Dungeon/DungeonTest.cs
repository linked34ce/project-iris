using NUnit.Framework;

public class DungeonTest
{
    [Test]
    [Category("Dungeon")]
    public void Instantiate()
    {
        var map = new Walls[][] {
            // 1
            new Walls[] {
                new(0, 1, 1, 0),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 0),
                new(0, 1, 0, 0),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(1, 1, 0, 1),
                new(1, 1, 1, 0),
            },
            // 2
            new Walls[] {
                new(1, 0, 1, 0),
                new(0, 1, 1, 0),
                new(1, 1, 0, 0),
                new(1, 1, 1, 0),
                new(0, 0, 1, 0),
                new(1, 0, 0, 0),
                new(1, 1, 1, 0),
                new(0, 1, 1, 0),
                new(1, 1, 0, 0),
                new(1, 0, 1, 0),
            },
            // 3
            new Walls[] {
                new(1, 0, 1, 0),
                new(0, 0, 1, 0),
                new(1, 0, 0, 0),
                new(1, 0, 1, 1),
                new(0, 0, 1, 0),
                new(1, 0, 0, 0),
                new(1, 0, 1, 1),
                new(0, 0, 1, 0),
                new(1, 0, 0, 0),
                new(1, 0, 1, 0),
            },
            // 4
            new Walls[] {
                new(1, 0, 1, 0),
                new(0, 0, 1, 1),
                new(0, 0, 0, 1),
                new(0, 1, 0, 1),
                new(0, 0, 0, 0),
                new(0, 0, 0, 0),
                new(0, 1, 0, 1),
                new(0, 0, 0, 1),
                new(1, 0, 0, 1),
                new(1, 0, 1, 0),
            },
            // 5
            new Walls[] {
                new(1, 0, 1, 0),
                new(0, 1, 0, 0),
                new(0, 1, 0, 1),
                new(1, 1, 0, 1),
                new(0, 0, 1, 0),
                new(1, 0, 0, 0),
                new(0, 1, 1, 1),
                new(0, 1, 0, 1),
                new(1, 1, 0, 1),
                new(1, 0, 1, 0),
            },
            // 6
            new Walls[] {
                new(1, 0, 1, 0),
                new(0, 1, 1, 0),
                new(0, 1, 0, 0),
                new(0, 1, 0, 1),
                new(0, 0, 0, 0),
                new(0, 0, 0, 0),
                new(0, 1, 0, 1),
                new(0, 1, 0, 0),
                new(1, 1, 0, 0),
                new(1, 0, 1, 0),
            },
            // 7
            new Walls[] {
                new(1, 0, 1, 0),
                new(0, 0, 1, 0),
                new(1, 0, 0, 0),
                new(1, 1, 1, 0),
                new(0, 0, 1, 0),
                new(1, 0, 0, 0),
                new(1, 1, 1, 0),
                new(0, 0, 1, 0),
                new(1, 0, 0, 0),
                new(1, 0, 1, 0),
            },
            // 8
            new Walls[] {
                new(1, 0, 1, 0),
                new(0, 0, 1, 1),
                new(1, 0, 0, 1),
                new(1, 0, 1, 1),
                new(0, 0, 1, 0),
                new(1, 0, 0, 0),
                new(1, 0, 1, 1),
                new(0, 0, 1, 1),
                new(1, 0, 0, 1),
                new(1, 0, 1, 0),
            },
            // 9
            new Walls[] {
                new(1, 0, 1, 0),
                new(0, 1, 1, 1),
                new(0, 1, 2, 1),
                new(0, 1, 0, 1),
                new(0, 0, 0, 1),
                new(0, 0, 0, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(1, 1, 0, 1),
                new(1, 0, 1, 0),
            },
            // 10
            new Walls[] {
                new(0, 0, 1, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(0, 1, 0, 1),
                new(1, 0, 0, 1),
            },
        };

        var dungeon = new Dungeon();

        Assert.That(dungeon, Is.Not.Null);
        Assert.That(dungeon, Is.InstanceOf<Dungeon>());

        Assert.That(dungeon.Name, Is.EqualTo("桃鳳学園 旧校舎"));
        Assert.That(dungeon.EncountRate, Is.EqualTo(0.1f));

        for (var i = 0; i < dungeon.Map.Length; i++)
        {
            for (var j = 0; j < dungeon.Map[i].Length; j++)
            {
                Assert.That(dungeon.Map[i][j].East, Is.EqualTo(map[i][j].East));
                Assert.That(dungeon.Map[i][j].South, Is.EqualTo(map[i][j].South));
                Assert.That(dungeon.Map[i][j].West, Is.EqualTo(map[i][j].West));
                Assert.That(dungeon.Map[i][j].North, Is.EqualTo(map[i][j].North));
            }
        }
    }
}
