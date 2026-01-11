namespace CADBooster.SolidDna;

/// <summary>
/// Represents a core SolidDNA object, that is disposable
/// and needs a COM object disposing cleanly on disposal
/// 
/// NOTE: Use this shared type if another part of the application may have access to this
///       same COM object and the lifecycle for each reference is managed independently
/// </summary>
public interface ISharedSolidDnaObject<out T> : ISolidDnaObject<T>
{
}