namespace CADBooster.SolidDna
{
    /// <summary>
    /// Arguments for when <see cref="ICommandManagerItem.OnStateCheck"/> is called.
    /// Change <see cref="Result"/>> to modify the state of the command item, flyout item or the whole flyout.
    /// Disabling the flyout only works when all items in the flyout are disabled.
    /// </summary>
    public class CommandManagerItemStateCheckArgs
    {
        /// <summary>
        /// Callback id of item. Used for find an associative <see cref="ICommandManagerItem"/>
        /// </summary>
        public string CallbackId { get; }

        /// <summary>
        /// Result returned to SolidWorks, determines the items state.
        /// Set this property to enable/disable a button or flyout.
        /// </summary>
        public CommandManagerItemState Result { get; set; } = CommandManagerItemState.DeselectedEnabled;

        /// <summary>
        /// Create a state check arguments object.
        /// </summary>
        /// <param name="callbackId">Callback id of item</param>
        public CommandManagerItemStateCheckArgs(string callbackId)
        {
            CallbackId = callbackId;
        }
    }
}
