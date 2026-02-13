using System.Runtime.InteropServices;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a core SolidDNA object, that is disposable
/// and needs a COM object disposing cleanly on disposal
/// </summary>
public class SolidDnaObject : ISolidDnaObject
{
    #region Protected Members

    /// <summary>
    /// A COM objects that should be cleanly disposed on disposing
    /// </summary>
    protected object mBaseObject;

    #endregion

    #region Public Properties

    /// <summary>
    /// The raw underlying COM object
    /// WARNING: Use with caution. You must handle all disposal from this point on
    /// </summary>
    public object UnsafeObject => mBaseObject;

    #endregion
}

/// <summary>
/// Represents a core SolidDNA object, that is disposable
/// and needs a COM object disposing cleanly on disposal
/// </summary>
public class SolidDnaObject<T> : SolidDnaObject, ISolidDnaObject<T>
{
    #region Protected Properties

    /// <summary>
    /// A COM objects that should be cleanly disposed on disposing
    /// </summary>
    protected T BaseObject
    {
        get => (T) mBaseObject;
        set => mBaseObject = value;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The raw underlying COM object
    /// WARNING: Use with caution. You must handle all disposal from this point on
    /// </summary>
    public new T UnsafeObject => BaseObject;

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="comObject">The COM object to wrap</param>
    public SolidDnaObject(T comObject)
    {
        BaseObject = comObject;
    }

    #endregion

    #region Dispose

    /// <inheritdoc />
    public virtual void Dispose()
    {
        if (BaseObject == null)
            return;

        // Do any specific disposal
        SolidDnaObjectDisposal.Dispose<T>(BaseObject);

        // Only release if it's actually a COM object (Moq/mocks are not RCWs)
        if (Marshal.IsComObject(BaseObject))
        {
            // COM release object. Calling Marshal.FinalReleaseComObject caused other add-ins to malfunction, so we use the less aggressive option.
            Marshal.ReleaseComObject(BaseObject);
        }

        // Clear reference
        BaseObject = default;
    }

    #endregion
}