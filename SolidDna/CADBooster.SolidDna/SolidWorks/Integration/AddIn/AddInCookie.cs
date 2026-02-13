namespace CADBooster.SolidDna;

/// <summary>
/// A wrapper around the cookie integer value returned by SolidWorks when registering an add-in.
/// Is used by the <see cref="CommandManager"/> to link menu items to the add-in that owns them.
/// </summary>
public class AddInCookie
{
    /// <summary>
    /// The value of the cookie. Is usually a single-digit integer.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// A wrapper around the cookie integer value returned by SolidWorks when registering an add-in.
    /// Is used by the <see cref="CommandManager"/> to link menu items to the add-in that owns them.
    /// </summary>
    /// <param name="cookieValue"></param>
    public AddInCookie(int cookieValue)
    {
        Value = cookieValue;
    }

    // <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is AddInCookie other)
            return Equals(other);

        return false;
    }

    /// <summary>
    /// Determines whether the specified <see cref="AddInCookie"/> is equal to the current <see cref="AddInCookie"/>.
    /// </summary>
    /// <param name="other">The cookie to compare with the current cookie</param>
    /// <returns>True if the values are equal.</returns>
    protected bool Equals(AddInCookie other) => Value == other.Value;

    // <inheritdoc />
    public override int GetHashCode() => Value;

    // <inheritdoc />
    public override string ToString() => $"Add-in cookie with value {Value}";
}