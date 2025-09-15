using NUnit.Framework;

public class LocationTest
{
    [Test]
    [Category("Location")]
    public void Instantiate()
    {
        var location = new Location();
        Assert.That(location, Is.Not.Null);
        Assert.That(location, Is.InstanceOf<Location>());

        Assert.That(location.X, Is.EqualTo(9));
        Assert.That(location.Y, Is.EqualTo(0));
    }

    [Test]
    [Category("Location")]
    public void ResetPosition()
    {
        var location = new Location
        {
            X = 10,
            Y = 10
        };

        location.ResetPosition();

        Assert.That(location.X, Is.EqualTo(9));
        Assert.That(location.Y, Is.EqualTo(0));
    }
}
