using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a SolidWorks selection manager. Used for selecting, deselecting and getting selected objects.
    /// </summary>
    public class SelectionManager : SolidDnaObject<SelectionMgr>
    {
        #region Private members

        /// <summary>
        /// The model this selection manager belongs to.
        /// </summary>
        private readonly Model mModel;

        /// <summary>
        /// The model extension this selection manager belongs to.
        /// </summary>
        private readonly ModelExtension mModelExtension;

        #endregion

        #region Constructor

        /// <summary>
        /// Old constructor
        /// </summary>
        [Obsolete("Use the full constructor that takes a model and model extension. This enables selecting and deselecting methods.")]
        public SelectionManager(SelectionMgr selectionMgr) : base(selectionMgr)
        {

        }

        /// <summary>
        /// Create a selection manager tied to a model and model extension.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="model"></param>
        /// <param name="modelExtension"></param>
        public SelectionManager(SelectionMgr manager, Model model, ModelExtension modelExtension) : base(manager)
        {
            mModel = model;
            mModelExtension = modelExtension;
        }

        #endregion

        #region Deselect

        /// <summary>
        /// Deselect all selected objects.
        /// </summary>
        public void DeselectAll() => mModel.UnsafeObject.ClearSelection2(true);

        /// <summary>
        /// Deselect the object at a given index with an optional selection mark.
        /// </summary>
        /// <param name="index">The index of the selected object. Is 1-based.</param>
        /// <param name="selectionMark">The mark that the item was originally selected with.
        /// Use <see cref="SelectionMark.Any"/> to ignore the mark and <see cref="SelectionMark.None"/> to deselect an object that was selected without a mark.</param>
        /// <returns>True if the object was deselected, false if not.</returns>
        public bool DeselectAtIndex(int index, SelectionMark selectionMark = SelectionMark.Any)
        {
            // Deselect the object
            var result = BaseObject.DeSelect2(index, (int)selectionMark);

            // Return whether it was successful: 1 means successful, 0 means not successful.
            return result == 1;
        }

        /// <summary>
        /// Deselect multiple objects at given indexes with an optional selection mark.
        /// </summary>
        /// <param name="indexes">The indexes of the selected objects. Are 1-based.</param>
        /// <param name="selectionMark">The mark that the items were originally selected with.
        /// Use <see cref="SelectionMark.Any"/> to ignore the mark and <see cref="SelectionMark.None"/> to deselect objects that were selected without a mark.</param>
        /// <returns>True if the objects were deselected, false if not.</returns>
        public bool DeselectAtIndexes(List<int> indexes, SelectionMark selectionMark = SelectionMark.Any)
        {
            // Deselect the objects
            var result = BaseObject.DeSelect2(indexes.ToArray(), (int)selectionMark);

            // Return whether it was successful: 1 means successful, 0 means not successful.
            return result == 1;
        }

        #endregion

        #region Get selected entities

        /// <summary>
        /// Enumerate through all selected objects in the current model with a certain selection mark.
        /// Allows you to stop enumerating early if needed.
        /// </summary>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        /// <returns>An enumerable of selected objects</returns>
        public IEnumerable<SelectedObject> EnumerateSelectedObjects(SelectionMark selectionMark = SelectionMark.Any)
        {
            // Get selection count
            var count = GetSelectedObjectCount(selectionMark);

            // If we have none, we are done
            if (count <= 0)
                yield break;

            // Otherwise, get all selected objects
            for (var i = 1; i <= count; i++)
            {
                // Get the object and its type.
                var selected = GetSelectedObjectAtIndex(i, selectionMark);

                // Return it
                yield return selected;
            }
        }

        /// <summary>
        /// Get the selected object at a given index and with a certain selection mark. Index is 1-based.
        /// </summary>
        /// <param name="index">The index of the selected object. Is 1-based.</param>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        /// <returns></returns>
        public SelectedObject GetSelectedObjectAtIndex(int index, SelectionMark selectionMark = SelectionMark.Any) =>
            new SelectedObject(BaseObject.GetSelectedObject6(index, (int)selectionMark), (swSelectType_e)BaseObject.GetSelectedObjectType3(index, (int)selectionMark));

        /// <summary>
        /// Get the number of selected objects in the current model with a certain selection mark.
        /// </summary>
        /// <param name="mark">Only include objects with this selection mark</param>
        /// <returns></returns>
        public int GetSelectedObjectCount(SelectionMark mark = SelectionMark.Any) => BaseObject.GetSelectedObjectCount2((int)mark);

        /// <summary>
        /// Get all selected objects in the current model with a certain selection mark.
        /// </summary>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        /// <returns>A list of all selected objects with the specified selection mark</returns>
        public List<SelectedObject> GetSelectedObjects(SelectionMark selectionMark = SelectionMark.Any)
        {
            // Create list
            var list = new List<SelectedObject>();

            // Get selection count
            var count = GetSelectedObjectCount(selectionMark);

            // Get all selected objects
            for (var i = 1; i <= count; i++)
            {
                // Get the object and its type. Add it to the list.
                list.Add(GetSelectedObjectAtIndex(i, selectionMark));
            }

            // Return the list
            return list;
        }

        /// <summary>
        /// Perform an action on all selected objects in the current model with any selection mark.
        /// </summary>
        /// <param name="action">The selected objects list to be worked on inside the action.
        /// NOTE: Do not store references to these objects outside of this action</param>
        /// <returns></returns>
        public void SelectedObjects(Action<List<SelectedObject>> action) => SelectedObjects(action, SelectionMark.Any);

        /// <summary>
        /// Perform an action on all selected objects in the current model with a certain selection mark.
        /// </summary>
        /// <param name="action">The selected objects list to be worked on inside the action.
        /// NOTE: Do not store references to these objects outside of this action</param>
        /// <param name="selectionMark"></param>
        /// <returns></returns>
        public void SelectedObjects(Action<List<SelectedObject>> action, SelectionMark selectionMark)
        {
            // Create list
            var list = new List<SelectedObject>();

            try
            {
                // Get selection count
                var count = GetSelectedObjectCount(selectionMark);

                // If we have none, we are done
                if (count <= 0)
                {
                    action(new List<SelectedObject>());
                    return;
                }

                // Get all selected objects
                for (var i = 1; i <= count; i++)
                {
                    // Get the object and its type. Add it to the list.
                    list.Add(GetSelectedObjectAtIndex(i, selectionMark));
                }

                // Call the action
                action(list);
            }
            finally
            {
                // Dispose of all items
                list.ForEach(f => f.Dispose());
            }
        }

        #endregion
    }
}
