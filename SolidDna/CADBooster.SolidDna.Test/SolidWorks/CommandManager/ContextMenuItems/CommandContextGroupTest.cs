using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class CommandContextGroupTest
{
    [Test]
    public void Create_WithNullInfo_ThrowsException()
    {
        var group = new CommandContextGroup
        {
            Name = "TestGroup",
            Items = new List<ICommandCreatable>
            {
                new CommandContextItem { Name = "Item1" }
            }
        };

        Assert.Throws<SolidDnaException>(() => group.Create(null));
    }

    [Test]
    public void Create_WithNullOrEmptyName_ThrowsException()
    {
        var group = new CommandContextGroup
        {
            Name = null,
            Items = new List<ICommandCreatable>
            {
                new CommandContextItem { Name = "Item1" }
            }
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => group.Create(info.Object));
    }

    [Test]
    public void Create_WithWhitespaceName_ThrowsException()
    {
        var group = new CommandContextGroup
        {
            Name = "   ",
            Items = new List<ICommandCreatable>
            {
                new CommandContextItem { Name = "Item1" }
            }
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => group.Create(info.Object));
    }

    [Test]
    public void Create_WithNullItems_ThrowsException()
    {
        var group = new CommandContextGroup
        {
            Name = "TestGroup",
            Items = null
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => group.Create(info.Object));
    }

    [Test]
    public void Create_WithEmptyItems_ThrowsException()
    {
        var group = new CommandContextGroup
        {
            Name = "TestGroup",
            Items = []
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => group.Create(info.Object));
    }

    [Test]
    public void ToString_ReturnsCorrectFormat()
    {
        var group = new CommandContextGroup
        {
            Name = "TestGroup",
            Items = new List<ICommandCreatable>
            {
                new CommandContextItem { Name = "Item1" },
                new CommandContextItem { Name = "Item2" }
            }
        };

        var result = group.ToString();

        Assert.That(result, Does.Contain("TestGroup"));
        Assert.That(result, Does.Contain("2"));
    }

    [Test]
    public void ToString_WithNullItems_DoesNotThrow()
    {
        var group = new CommandContextGroup
        {
            Name = "TestGroup",
            Items = null
        };

        var result = group.ToString();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Does.Contain("TestGroup"));
    }
}
