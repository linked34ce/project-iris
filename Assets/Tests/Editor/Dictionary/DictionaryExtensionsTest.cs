using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class DictionaryExtensionsTest
{
    [Test]
    [Category("DictionaryExtensions")]
    public void GetValueOrDefault()
    {
        Dictionary<string, int> dictionary = new(){
            {"a", 1},
            {"b", 2},
        };

        Assert.That(dictionary.GetValueOrDefault("a"), Is.EqualTo(1));
        Assert.That(dictionary.GetValueOrDefault("b"), Is.EqualTo(2));
        Assert.That(dictionary.GetValueOrDefault("c", 0), Is.EqualTo(0));
    }
}
