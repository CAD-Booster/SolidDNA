using NUnit.Framework;
using System;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class ArrayExtensionsTest
{
    [Test]
    public void Append_Null_Appends()
    {
        int[] source = null;
        // ReSharper disable once ExpressionIsAlwaysNull
        var result = source.Append(1, 2);
        Assert.That(result, Is.EqualTo([1, 2]));
    }

    [Test]
    public void Append_Empty_Appends()
    {
        var source = Array.Empty<int>();
        var result = source.Append(3, 4);
        Assert.That(result, Is.EqualTo([3, 4]));
    }

    [Test]
    public void Append_Normal_AppendOne()
    {
        var source = new[] { 5, 6 };
        var result = source.Append(7);
        Assert.That(result, Is.EqualTo([5, 6, 7]));
    }

    [Test]
    public void Append_Normal_AppendMultiple()
    {
        var source = new[] { 5, 6 };
        var result = source.Append(7, 8);
        Assert.That(result, Is.EqualTo([5, 6, 7, 8]));
    }

    [Test]
    public void Prepend_Null_Prepends()
    {
        int[] source = null;
        // ReSharper disable once ExpressionIsAlwaysNull
        var result = source.Prepend(1, 2);
        Assert.That(result, Is.EqualTo([1, 2]));
    }

    [Test]
    public void Prepend_Empty_Prepends()
    {
        var source = Array.Empty<int>();
        var result = source.Prepend(3, 4);
        Assert.That(result, Is.EqualTo([3, 4]));
    }

    [Test]
    public void Prepend_Normal_PrependOne()
    {
        var source = new[] { 5, 6 };
        var result = source.Prepend(7);
        Assert.That(result, Is.EqualTo([7, 5, 6]));
    }

    [Test]
    public void Prepend_Normal_PrependMultiple()
    {
        var source = new[] { 5, 6 };
        var result = source.Prepend(7, 8);
        Assert.That(result, Is.EqualTo([7, 8, 5, 6]));
    }
}