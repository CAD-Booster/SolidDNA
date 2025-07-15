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
        /// Gets all the selected objects in the model
        /// </summary>
        /// <param name="action">The selected objects list to be worked on inside the action.
        ///     NOTE: Do not store references to them outside of this action</param>
        /// <returns></returns>
        public void SelectedObjects(Action<List<SelectedObject>> action)
        {
            // Create list
            var list = new List<SelectedObject>();

            // Get selected objects with any mark value
            const int anyMark = -1;

            try
            {
                // Get selection count
                var count = BaseObject.GetSelectedObjectCount2(anyMark);

                // If we have none, we are done
                if (count <= 0)
                {
                    action(new List<SelectedObject>());
                    return;
                }

                // Get all selected objects
                for (var i = 1; i <= count; i++)
                {
                    // Get the object and its type
                    var selected = new SelectedObject(BaseObject.GetSelectedObject6(i, anyMark), (swSelectType_e)BaseObject.GetSelectedObjectType3(i, anyMark));

                    // Add to the list
                    list.Add(selected);
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
