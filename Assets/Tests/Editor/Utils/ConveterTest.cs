using NUnit.Framework;

public class ConverterTest
{
    [Test]
    [Category("Converter")]
    public void ToOrdinal()
    {
        Assert.That(Converter.ToOrdinal(1), Is.EqualTo("1st"));
        Assert.That(Converter.ToOrdinal(2), Is.EqualTo("2nd"));
        Assert.That(Converter.ToOrdinal(3), Is.EqualTo("3rd"));
        Assert.That(Converter.ToOrdinal(4), Is.EqualTo("4th"));
        Assert.That(Converter.ToOrdinal(10), Is.EqualTo("10th"));
        Assert.That(Converter.ToOrdinal(11), Is.EqualTo("11th"));
        Assert.That(Converter.ToOrdinal(12), Is.EqualTo("12th"));
        Assert.That(Converter.ToOrdinal(13), Is.EqualTo("13th"));
        Assert.That(Converter.ToOrdinal(14), Is.EqualTo("14th"));
        Assert.That(Converter.ToOrdinal(20), Is.EqualTo("20th"));
        Assert.That(Converter.ToOrdinal(21), Is.EqualTo("21st"));
        Assert.That(Converter.ToOrdinal(22), Is.EqualTo("22nd"));
        Assert.That(Converter.ToOrdinal(23), Is.EqualTo("23rd"));
        Assert.That(Converter.ToOrdinal(24), Is.EqualTo("24th"));
    }
}
