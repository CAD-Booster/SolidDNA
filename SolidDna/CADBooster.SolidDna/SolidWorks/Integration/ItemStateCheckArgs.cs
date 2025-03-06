using System;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Arguments used in EnableMethod
    /// Change Result to modify command item or flyout state
    /// </summary>
    public class ItemStateCheckArgs : EventArgs
    {
        /// <summary>
        /// Result returned to SolidWorks, determines the items state
        /// </summary>
        public ItemState Result { get; set; } = ItemState.DeselectedEnabled;

        /// <summary>
        /// Callback id of item. Used for find an associative <see cref="ICommandManagerItem"/>
        /// </summary>
        public string CallbackId { get; }

        /// <summary>
        /// Create a state check arguments
        /// </summary>
        /// <param name="callbackId">Callback id of item</param>
        public ItemStateCheckArgs(string callbackId)
        {
            CallbackId = callbackId;
        }
    }
}
