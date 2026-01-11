using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections;
using System.Linq;

namespace CADBooster.SolidDna;

/// <summary>
/// The ID of a selectable model object (view, sheet, edge) in a series of bytes.
/// Is called a Persistent Reference ID by SolidWorks. Is often 20 bytes long, but this varies per object type.
/// Is pretty constant (hence the name) but can change after rebuild, so still not a perfect way of comparing objects.
/// See https://help.solidworks.com/2026/english/api/sldworksapiprogguide/overview/Persistent_Reference_IDs.htm for more information.
/// See https://help.solidworks.com/2026/english/api/swconst/SolidWorks.interop.swconst~SolidWorks.interop.swconst.swSelectType_e.html for all selectable objects types.
/// </summary>
public class PersistentId
{
    #region Public members

    /// <summary>
    /// Minimum number of integers in the persistent reference ID.
    /// </summary>
    public const int MinimumArrayLength = 16;

    #endregion

    #region Public Properties

    /// <summary>
    /// Get the underlying byte array from this persistent ID, which is the form that SolidWorks uses.
    /// </summary>
    /// <returns></returns>
    public byte[] ByteArray { get; }

    #endregion

    #region Constructor

    /// <summary>
    /// Create a persistent ID from a byte array. Use <see cref="GetFromObject"/> when you can, instead of this constructor.
    /// </summary>
    /// <param name="byteArray"></param>
    /// <exception cref="Exception"></exception>
    public PersistentId(byte[] byteArray)
    {
        // Check if the byte array is long enough
        if (byteArray.Length < MinimumArrayLength)
            throw new SolidDnaException(SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.Identification,
                SolidDnaErrorCode.IdentificationArrayTooShortForPersistentId, $"Byte array needs to be at least {MinimumArrayLength} bytes long"));

        ByteArray = byteArray;
    }

    /// <summary>
    /// Create a persistent ID from a byte array object. Use <see cref="GetFromObject"/> when you can, instead of this constructor.
    /// </summary>
    /// <param name="byteArrayObject"></param>
    /// <exception cref="Exception"></exception>
    public PersistentId(object byteArrayObject)
    {
        // Cast the object to a byte array
        var byteArray = CastToByteArray(byteArrayObject);

        // Check if the byte array is long enough
        if (byteArray.Length < MinimumArrayLength)
            throw new SolidDnaException(SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.Identification,
                SolidDnaErrorCode.IdentificationArrayTooShortForPersistentId, $"Byte array needs to be at least {MinimumArrayLength} bytes long"));

        ByteArray = byteArray;
    }

    #endregion

    #region Get the persistent ID from an object

    /// <summary>
    /// Find the persistent reference of a SolidWorks object (sheet, view etc.).
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static PersistentId GetFromObject(object obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        // Get the raw persistent reference object
        var persistentRefObject = SolidWorksEnvironment.IApplication.ActiveModel.Extension.UnsafeObject.GetPersistReference3(obj);

        // Make sure it's not null
        if (persistentRefObject == null)
            throw new SolidDnaException(SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.Identification, SolidDnaErrorCode.IdentificationPersistentIdNotFound));

        // Wrap it in a persistent ID object
        return GetFromByteArrayObject(persistentRefObject);
    }

    #endregion

    #region Get an object by its persistent ID

    /// <summary>
    /// Get a component by its persistent ID and wrap it in our own Component class. Throws when it fails.
    /// </summary>
    /// <returns></returns>
    public Component GetComponent()
    {
        // Get the underlying Component2 object. Throws when it fails.
        var component = GetObject<Component2>();

        // If we get here, we have an object. Now wrap it.
        return new Component(component);
    }

    /// <summary>
    /// Get a configuration by its persistent ID and wrap it in our own ModelConfiguration class. Throws when it fails.
    /// </summary>
    /// <returns></returns>
    public ModelConfiguration GetConfiguration()
    {
        // Get the underlying feature object, which is the base type for a Configuration. Throws when it fails.
        var feature = GetObject<Feature>();

        // If we get here, we have an object. Convert it to a Configuration object.
        var configuration = feature.GetSpecificFeature2() as Configuration;

        // Now wrap it.
        return new ModelConfiguration(configuration);
    }

    /// <summary>
    /// Get a display dimension by its persistent ID and wrap it in our own ModelDisplayDimension class. Throws when it fails.
    /// </summary>
    /// <returns></returns>
    public ModelDisplayDimension GetDimension()
    {
        // Get the underlying display dimension object. Throws when it fails.
        var displayDimension = GetObject<DisplayDimension>();

        // If we get here, we have an object. Now wrap it.
        return new ModelDisplayDimension(displayDimension);
    }

    /// <summary>
    /// Get a feature by its persistent ID and wrap it in our own ModelFeature class. Throws when it fails.
    /// </summary>
    /// <returns></returns>
    public ModelFeature GetFeature()
    {
        // Get the underlying feature object. Throws when it fails.
        var feature = GetObject<Feature>();

        // If we get here, we have an object. Now wrap it.
        return new ModelFeature(feature);
    }

    /// <summary>
    /// Get a note by its persistent ID and wrap it in our own Note class. Throws when it fails.
    /// </summary>
    /// <returns></returns>
    public Note GetNote()
    {
        // Get the underlying note object. Throws when it fails.
        var note = GetObject<INote>();

        // If we get here, we have an object. Now wrap it.
        return new Note(note);
    }

    /// <summary>
    /// Get a sketch by its persistent ID and wrap it in our own FeatureSketch class. Throws when it fails.
    /// </summary>
    /// <returns></returns>
    public FeatureSketch GetSketch()
    {
        // Get the underlying feature object, which is the base type for a Sketch. Throws when it fails.
        var feature = GetObject<Feature>();

        // If we get here, we have an object. Convert it to a Sketch object.
        var sketch = feature.GetSpecificFeature2() as Sketch;

        // Now wrap it.
        return new FeatureSketch(sketch);
    }

    /// <summary>
    /// Find an object (sheet, view etc.) by its persistent reference. Throws when it fails.
    /// Returns the base class of the object, so a Feature for configurations, sketches, reference planes, etc.
    /// </summary>
    /// <returns></returns>
    public T GetObject<T>() where T : class
    {
        // Try to get an object by its persistent ID
        var obj = SolidWorksEnvironment.IApplication.ActiveModel.Extension.UnsafeObject.GetObjectByPersistReference3(ByteArray, out var errorCode);

        // If there is no error, return the object
        if (errorCode == 0)
            return (T)obj;

        // Convert the integer error code to an enum
        var errorCodeEnum = (swPersistReferencedObjectStates_e)errorCode;

        // Get the useful part of the error code
        var errorText = errorCodeEnum.ToString().Substring(26);

        // If there is an error, throw an exception
        throw new SolidDnaException(SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.Identification, SolidDnaErrorCode.IdentificationObjectNotFoundFromPersistentId, $"Error code: {errorText}"));
    }

    /// <summary>
    /// Find an object (sheet, view etc.) by its persistent ID or return null when it fails.
    /// </summary>
    /// <returns></returns>
    public T GetObjectOrNull<T>() where T : class
    {
        return (T)SolidWorksEnvironment.IApplication.ActiveModel?.Extension?.UnsafeObject.GetObjectByPersistReference3(ByteArray, out var errorCode);
    }

    #endregion

    #region Equals, GetHashCode and ToString

    /// <summary>
    /// Get if this persistent ID is equal to another persistent ID.
    /// </summary>
    /// <param name="otherId"></param>
    /// <returns></returns>
    public bool Equals(PersistentId otherId)
    {
        return otherId != null && ByteArray.SequenceEqual(otherId.ByteArray);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(PersistentId))
            return false;
        return Equals((PersistentId)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        // Use the hash code of the byte array
        return ByteArray.Aggregate(0, (current, b) => current ^ b.GetHashCode());
    }

    /// <summary>
    /// Convert the persistent ID to a string with dashes between the bytes.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        const char dash = '-';

        // Create a string with dashes between the bytes
        var bytesWithDashes = ByteArray.Aggregate("", (current, b) => current + b + dash);

        // Remove the last dash
        return bytesWithDashes.TrimEnd(dash);
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Create a new persistent ID from a byte array object.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static PersistentId GetFromByteArrayObject(object obj)
    {
        try
        {
            return new PersistentId(obj);
        }
        catch (Exception e)
        {
            throw new SolidDnaException(SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.Identification,
                SolidDnaErrorCode.IdentificationArrayTooShortForPersistentId, "Error creating persistent ID", e));
        }
    }

    /// <summary>
    /// Convert the input (SolidWorks uses a byte array in an object) to a real byte array.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private static byte[] CastToByteArray(object id)
    {
        return ((IEnumerable)id).Cast<byte>().ToArray();
    }

    #endregion
}