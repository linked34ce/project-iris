using NUnit.Framework;

public class MathTest
{
    [Test]
    public void Addition_Works()
    {
        int a = 2;
        int b = 3;
        Assert.AreEqual(5, a + b);
    }
}
