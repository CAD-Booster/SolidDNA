using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a SolidWorks selection manager
    /// </summary>
    public class SelectionManager : SolidDnaObject<SelectionMgr>
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

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
        [Obsolete("Suppressed by EnumerateSelectedObjects")]
        public void SelectedObjects(Action<List<SelectedObject>> action)
        {
            // Create list 
            var list = new List<SelectedObject>();
            try
            {
                // Call the action
                action(EnumerateSelectedObjects().ToList());
            }
            finally
            {
                // Dispose of all items
                list.ForEach(f => f.Dispose());
            }
        }

        public IEnumerable<SelectedObject> EnumerateSelectedObjects()
        {
            var mark = -1;
            var count = BaseObject.GetSelectedObjectCount2(mark);

            // If we have none, we are done
            if (count <= 0)
                yield break;

            // Otherwise, get all selected objects
            for (var i = 1; i < count; i++)
            {
                var selectedBase = BaseObject.GetSelectedObject6(i, mark);

                if (selectedBase is null)
                    continue;

                // Get the object itself
                var selected = new SelectedObject(selectedBase,
                                                  (swSelectType_e)BaseObject.GetSelectedObjectType3(i, mark));

                yield return selected;
            }
        }

        public void SelectObjects(IEnumerable<SelectedObject> selectedObjects, bool append = false, bool updateUserInterface = true)
        {
            if (!selectedObjects.Any())
                return;

            if (!append)
                BaseObject.SuspendSelectionList();

            using (var selectData = BaseObject.CreateSelectData().ToDnaObject())
            {
                BaseObject.AddSelectionListObjects(selectedObjects
                                                        .Select(x => x.UnsafeObject)
                                                        .Where(x => x != null)
                                                        .Select(x => new DispatchWrapper(x))
                                                        .ToArray(),
                                                    new DispatchWrapper(selectData.UnsafeObject));
            }

            if (updateUserInterface)
            {
                SolidWorksEnvironment.Application.ActiveModel.UnsafeObject.GraphicsRedraw();
                var featureManager = SolidWorksEnvironment.Application.ActiveModel.UnsafeObject.FeatureManager.ToDnaObject();
                featureManager.UnsafeObject.UpdateFeatureTree();
                featureManager.Dispose();
            }
        }

        public IDisposable TemporarySelectObjects(IEnumerable<SelectedObject> selectedObjects, bool updateUserInterface = true)
        {
            SelectObjects(selectedObjects, false, updateUserInterface);
            var disposable = new SuspendSelectionListDisposable(BaseObject, x => _disposables.Remove(x));

            _disposables.Add(disposable);
            return disposable;
        }

        private class SuspendSelectionListDisposable : IDisposable
        {
            private SelectionMgr _selectionMgr;
            private readonly Action<IDisposable> _onDisposed;

            public SuspendSelectionListDisposable(SelectionMgr selectionMgr, Action<IDisposable> onDisposed)
            {
                _selectionMgr = selectionMgr;
                _onDisposed = onDisposed;
            }

            public void Dispose()
            {
                if (_selectionMgr is null)
                    return;

                _selectionMgr.ResumeSelectionList2(false);
                _selectionMgr = null;
                _onDisposed.Invoke(this);
            }
        }
        #endregion
    }
}

