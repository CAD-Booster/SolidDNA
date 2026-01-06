namespace CADBooster.SolidDna
{
    /// <summary>
    /// When selecting an object, attach a Mark to it so you can distinguish the objects later.
    /// Used for example when you create a component pattern, where you use mark 1 for the seed components, mark 2 for direction 1 and mark 4 for direction 2.
    /// Use -1 or <see cref="Any"/> to get all selected objects from the <see cref="SelectionManager"/> regardless of their mark.
    /// This enum contains all selection mark that are mentioned in the documentation.
    /// </summary>
    public enum SelectionMark
    {
        /// <summary>
        /// Use this to get all selected objects regardless of their mark.
        /// </summary>
        Any = -1,

        /// <summary>
        /// When selecting: do not set a mark. When getting selection: get only objects with no selection mark.
        /// </summary>
        None = 0,

        /// <summary>
        /// Mark the selected object with mark 1.
        /// </summary>
        Mark1 = 1,

        /// <summary>
        /// Mark the selected object with mark 2.
        /// </summary>
        Mark2 = 2,
        
        /// <summary>
        /// Mark the selected object with mark 4.
        /// </summary>
        Mark4 = 4,
        
        /// <summary>
        /// Mark the selected object with mark 8.
        /// </summary>
        Mark8 = 8,

        /// <summary>
        /// Mark the selected object with mark 16.
        /// </summary>
        Mark16 = 16,

        /// <summary>
        /// Mark the selected object with mark 32.
        /// </summary>
        Mark32 = 32,

        /// <summary>
        /// Mark the selected object with mark 64.
        /// </summary>
        Mark64 = 64,

        /// <summary>
        /// Mark the selected object with mark 128.
        /// </summary>
        Mark128 = 128,

        /// <summary>
        /// Mark the selected object with mark 256.
        /// </summary>
        Mark256 = 256,

        /// <summary>
        /// Mark the selected object with mark 512.
        /// </summary>
        Mark512 = 512,

        /// <summary>
        /// Mark the selected object with mark 1024.
        /// </summary>
        Mark1024 = 1024,

        /// <summary>
        /// Mark the selected object with mark 2048.
        /// </summary>
        Mark2048 = 2048,

        /// <summary>
        /// Mark the selected object with mark 4096.
        /// </summary>
        Mark4096 = 4096,

        /// <summary>
        /// Mark the selected object with mark 8192.
        /// </summary>
        Mark8192 = 8192,

        /// <summary>
        /// Mark the selected object with mark 16384.
        /// </summary>
        Mark16384 = 16384,

        /// <summary>
        /// Mark the selected object with mark 32768.
        /// </summary>
        Mark32768 = 32768,

        /// <summary>
        /// Mark the selected object with mark 65536.
        /// </summary>
        Mark65536 = 65536,

        /// <summary>
        /// Mark the selected object with mark 131072.
        /// </summary>
        Mark131072 = 131072,
    }
}
