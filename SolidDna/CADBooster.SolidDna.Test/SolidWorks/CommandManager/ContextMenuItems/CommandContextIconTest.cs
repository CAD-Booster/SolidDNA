using Moq;
using NUnit.Framework;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class CommandContextIconTest
{
    [Test]
    public void Create_WithNullInfo_ThrowsException()
    {
        var icon = new CommandContextIcon
        {
            Hint = "TestIcon",
            IconPathFormat = @"C:\Icons\icon{0}.png"
        };

        Assert.Throws<SolidDnaException>(() => icon.Create(null));
    }

    [Test]
    public void Create_WithNullOrEmptyHint_ThrowsException()
    {
        var icon = new CommandContextIcon
        {
            Hint = null,
            IconPathFormat = @"C:\Icons\icon{0}.png"
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => icon.Create(info.Object));
    }

    [Test]
    public void Create_WithWhitespaceHint_ThrowsException()
    {
        var icon = new CommandContextIcon
        {
            Hint = "   ",
            IconPathFormat = @"C:\Icons\icon{0}.png"
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => icon.Create(info.Object));
    }

    [Test]
    public void Create_WithNullOrEmptyIconPathFormat_ThrowsException()
    {
        var icon = new CommandContextIcon
        {
            Hint = "TestIcon",
            IconPathFormat = null
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => icon.Create(info.Object));
    }

    [Test]
    public void Create_WithWhitespaceIconPathFormat_ThrowsException()
    {
        var icon = new CommandContextIcon
        {
            Hint = "TestIcon",
            IconPathFormat = "   "
        };
        var info = new Mock<ICommandContextCreateInfo>();
        info.Setup(x => x.SolidWorksCookie).Returns(1);

        Assert.Throws<SolidDnaException>(() => icon.Create(info.Object));
    }

    [Test]
    public void VisibleForAssemblies_DefaultValue_IsTrue()
    {
        var icon = new CommandContextIcon();

        Assert.That(icon.VisibleForAssemblies, Is.True);
    }

    [Test]
    public void VisibleForDrawings_DefaultValue_IsTrue()
    {
        var icon = new CommandContextIcon();

        Assert.That(icon.VisibleForDrawings, Is.True);
    }

    [Test]
    public void VisibleForParts_DefaultValue_IsTrue()
    {
        var icon = new CommandContextIcon();

        Assert.That(icon.VisibleForParts, Is.True);
    }

    [Test]
    public void SelectionType_DefaultValue_IsEverything()
    {
        var icon = new CommandContextIcon();

        Assert.That(icon.SelectionType, Is.EqualTo(SelectionType.Everything));
    }

    [Test]
    public void Name_ReturnsHintValue()
    {
        var icon = new CommandContextIcon
        {
            Hint = "TestHint"
        };

        ICommandCreatable creatable = icon;
        Assert.That(creatable.Name, Is.EqualTo("TestHint"));
    }
}
