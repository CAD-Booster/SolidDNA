namespace CADBooster.SolidDna;

/// <summary>
/// A wrapper around the cookie integer value returned by SolidWorks when registering an add-in.
/// Is used by the <see cref="CommandManager"/> to link menu items to the add-in that owns them.
/// </summary>
public class AddInCookie
{
    /// <summary>
    /// A wrapper around the cookie integer value returned by SolidWorks when registering an add-in.
    /// Is used by the <see cref="CommandManager"/> to link menu items to the add-in that owns them.
    /// </summary>
    /// <param name="cookieValue"></param>
    public AddInCookie(int cookieValue)
    {
        Value = cookieValue;
    }

    /// <summary>
    /// The value of the cookie. Is usually a single-digit integer.
    /// </summary>
    public int Value { get; }
}