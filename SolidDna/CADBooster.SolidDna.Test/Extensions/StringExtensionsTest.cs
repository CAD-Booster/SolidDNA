using NUnit.Framework;
using System.Collections.Generic;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class StringExtensionsTest
{
    [TestCase(null, true)]
    [TestCase("", true)]
    [TestCase(" ", false)]
    [TestCase("test", false)]
    public void IsNullOrEmpty(string input, bool expected)
    {
        var result = input.IsNullOrEmpty();

        Assert.That(expected, Is.EqualTo(result));
    }

    [TestCase(null, true)]
    [TestCase("", true)]
    [TestCase(" ", true)]
    [TestCase("test", false)]
    public void IsNullOrWhiteSpace(string input, bool expected)
    {
        var result = input.IsNullOrWhiteSpace();

        Assert.That(expected, Is.EqualTo(result));
    }

    [TestCase("Hello World", "hello world", true)]
    [TestCase("Hello World", "HELLO WORLD", true)]
    [TestCase("Hello World", "test", false)]
    public void ContainsIgnoreCase_ChecksIfListContainsString(string haystack, string needle, bool expected)
    {
        var list = new List<string> { haystack };

        var result = list.ContainsIgnoreCase(needle);

        Assert.That(expected, Is.EqualTo(result));
    }
}