namespace CADBooster.SolidDna;

/// <summary>
/// Extension method helpers for <see cref="SolidDnaObject{T}"/>
/// </summary>
public static class SolidDnaObjectExtensions
{
    /// <summary>
    /// Checks if the inner COM object is null. If so, returns null instead of 
    /// the created safe <see cref="SolidDnaObject{T}"/> object
    /// </summary>
    /// <typeparam name="T">The type of SolidDnaObject object being created</typeparam>
    /// <param name="createdObject">The instance that was created</param>
    /// <returns></returns>
    public static T CreateOrNull<T>(this T createdObject) where T : SolidDnaObject => createdObject?.UnsafeObject == null ? null : createdObject;
}