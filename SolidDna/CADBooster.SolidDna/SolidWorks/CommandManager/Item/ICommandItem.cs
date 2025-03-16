using System;

namespace CADBooster.SolidDna
{
    public interface ICommandItem
    {
        /// <summary>
        /// The unique Callback ID (set by creator)
        /// </summary>
        string CallbackId { get; }

        /// <summary>
        /// The action to call when the item is clicked
        /// </summary>
        Action OnClick { get; }

        /// <summary>
        /// The action to call when the item state requested
        /// </summary>
        Action<ItemStateCheckArgs> OnStateCheck { get; }
    }
}