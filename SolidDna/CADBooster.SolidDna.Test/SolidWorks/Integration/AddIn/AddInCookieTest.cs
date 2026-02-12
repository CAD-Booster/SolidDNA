using NUnit.Framework;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class AddInCookieTest
{
    [Test]
    public void Ctor_SetsValue()
    {
        var cookie = new AddInCookie(8);

        Assert.That(cookie.Value, Is.EqualTo(8));
        Assert.That(cookie.ToString(), Is.EqualTo("Add-in cookie with value 8"));
    }

    [Test]
    public void Equals()
    {
        var cookie1 = new AddInCookie(5);
        var cookie2 = new AddInCookie(5);
        var cookie3 = new AddInCookie(100);

        Assert.That(cookie1, Is.EqualTo(cookie2));
        Assert.That(cookie1, Is.Not.EqualTo(cookie3));
        Assert.That(cookie1, Is.Not.EqualTo(new object()));
        Assert.That(cookie1.GetHashCode(), Is.EqualTo(cookie2.GetHashCode()));
    }
}