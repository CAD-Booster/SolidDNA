using System;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Arguments used in EnableMethod
    /// Change Result to modify command item or flyout state
    /// </summary>
    public class CommandManagerItemStateCheckArgs : EventArgs
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
