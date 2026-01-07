namespace CADBooster.SolidDna
{
    /// <summary>
    /// A class providing shorthand access to commonly used values
    /// </summary>
    public static class SolidWorksEnvironment
    {
        /// <summary>
        /// The currently running instance of SolidWorks
        /// </summary>
        public static ISolidWorksApplication Application => AddInIntegration.SolidWorks;
    }
}
