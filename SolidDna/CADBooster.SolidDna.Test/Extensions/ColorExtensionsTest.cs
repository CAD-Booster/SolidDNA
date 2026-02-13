using NUnit.Framework;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class ColorExtensionsTest
{
    [TestCase(0, 0, 0, 0)] // 0x000000
    [TestCase(255, 0, 0, 255)] // 0x0000FF
    [TestCase(0, 255, 0, 65280)] // 0x00FF00
    [TestCase(0, 0, 255, 16711680)] // 0xFF0000
    [TestCase(255, 255, 255, 16777215)] // 0xFFFFFF
    public void ToColorRef_FromMediaColor(byte r, byte g, byte b, int expected)
    {
        var color = System.Windows.Media.Color.FromRgb(r, g, b);

        var colorRef = color.ToColorRef();

        Assert.That(colorRef, Is.EqualTo(expected));
    }

    [TestCase(0, 0, 0, 0)] // 0x000000
    [TestCase(255, 0, 0, 255)] // 0x0000FF
    [TestCase(0, 255, 0, 65280)] // 0x00FF00
    [TestCase(0, 0, 255, 16711680)] // 0xFF0000
    [TestCase(255, 255, 255, 16777215)] // 0xFFFFFF
    public void ToColorRef_FromDrawingColor(byte r, byte g, byte b, int expected)
    {
        var color = System.Drawing.Color.FromArgb(r, g, b);

        var colorRef = color.ToColorRef();

        Assert.That(colorRef, Is.EqualTo(expected));
    }
}