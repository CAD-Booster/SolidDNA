using Moq;
using NUnit.Framework;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class CommandContextItemTest
{
    [Test]
    public void Create_WithNullInfo_ThrowsException()
    {
        var item = new CommandContextItem
        {
            Name = "TestItem",
            Hint = "Test hint"
        };

        Assert.Throws<SolidDnaException>(() => item.Create(null));
    }

    [Test]
    public void Create_WithNullOrEmptyName_ThrowsException()
    {
        var item = new CommandContextItem
        {
            Name = null,
            Hint = "Test hint"
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => item.Create(info.Object));
    }

    [Test]
    public void Create_WithWhitespaceName_ThrowsException()
    {
        var item = new CommandContextItem
        {
            Name = "   ",
            Hint = "Test hint"
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => item.Create(info.Object));
    }

    [Test]
    public void VisibleForAssemblies_DefaultValue_IsTrue()
    {
        var item = new CommandContextItem();

        Assert.That(item.VisibleForAssemblies, Is.True);
    }

    [Test]
    public void VisibleForDrawings_DefaultValue_IsTrue()
    {
        var item = new CommandContextItem();

        Assert.That(item.VisibleForDrawings, Is.True);
    }

    [Test]
    public void VisibleForParts_DefaultValue_IsTrue()
    {
        var item = new CommandContextItem();

        Assert.That(item.VisibleForParts, Is.True);
    }

    [Test]
    public void SelectionType_DefaultValue_IsEverything()
    {
        var item = new CommandContextItem();

        Assert.That(item.SelectionType, Is.EqualTo(SelectionType.Everything));
    }
}
