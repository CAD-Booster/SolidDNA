using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a SolidWorks selection manager
    /// </summary>
    public class SelectionManager : SolidDnaObject<SelectionMgr>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SelectionManager(SelectionMgr model) : base(model)
        {

        }

        #endregion

        #region Selected Entities

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
