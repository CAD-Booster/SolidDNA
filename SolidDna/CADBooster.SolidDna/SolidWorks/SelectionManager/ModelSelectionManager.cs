using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Media.Media3D;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a SolidWorks selection manager
    /// </summary>
    public class SelectionManager : SolidDnaObject<SelectionMgr>
    {
        #region SuspendSelectionListDisposable

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

        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        private readonly Model _model;
        private readonly ModelExtension _modelExtension;

        public int SelectedObjectsCount => BaseObject.GetSelectedObjectCount2(-1);

        #region Constructor

        /// <summary>
        /// Constructor to create manager
        /// Contains several dependencies, since the selection functionality is scattered across many API objects. 
        /// It is better to concentrate all the selection functionality in one class.
        /// </summary>
        public SelectionManager(SelectionMgr manager, Model model, ModelExtension modelExtension) : base(manager)
        {
            _model = model;
            _modelExtension = modelExtension;
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
            => EnumerateSelectedObjects(-1);

        public IEnumerable<SelectedObject> EnumerateSelectedObjects(int mark)
        {
            var count = BaseObject.GetSelectedObjectCount2(mark);

            // If we have none, we are done
            if (count <= 0)
                yield break;

            // Otherwise, get all selected objects
            for (var i = 1; i <= count; i++)
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

        public void SelectAll() => _modelExtension.UnsafeObject.SelectAll();

        public void SelectObjects(IEnumerable<SelectedObject> selectedObjects, SelectionData selectionData, bool updateUserInterface = true)
        {
            if (!selectedObjects.Any())
                return;

            if (selectionData.Mode == SelectionMode.Override)
                _ = BaseObject.SuspendSelectionList();

            using (var selectData = BaseObject.CreateSelectData().ToDnaObject())
            {
                selectData.UnsafeObject.X = selectionData.Point.X; 
                selectData.UnsafeObject.Y = selectionData.Point.Y;
                selectData.UnsafeObject.Z = selectionData.Point.Z;
                selectData.UnsafeObject.Mark = selectionData.Mark;

                _ = BaseObject.AddSelectionListObjects(selectedObjects
                                                        .Select(x => x.UnsafeObject)
                                                        .Where(x => x != null)
                                                        .Select(x => new DispatchWrapper(x))
                                                        .ToArray(),
                                                   selectData.UnsafeObject);
            }
            
            if (updateUserInterface)
            {
                UpdateInterface();
            }
        }

        public void SelectObjects(IEnumerable<SelectedObject> selectedObjects, bool updateUserInterface = true)
            => SelectObjects(selectedObjects, SelectionData.Default, updateUserInterface);

        public void SelectObject(SelectedObject selectedObjects, SelectionData selectionData, bool updateUserInterface = true)
            => SelectObjects(Enumerable.Repeat(selectedObjects, 1), selectionData, updateUserInterface);

        public void SelectObject(string name, string selectionType, SelectionData selectionData)
        {
            // TODO: Implement selectionType

            _ = _modelExtension.UnsafeObject.SelectByID2(name,
                                                     selectionType,
                                                     selectionData.Point.X,
                                                     selectionData.Point.Y,
                                                     selectionData.Point.Z,
                                                     selectionData.Mode == SelectionMode.Append,
                                                     selectionData.Mark,
                                                     null,
                                                     (int)swSelectOption_e.swSelectOptionDefault);
        }

        // TODO: Implement selectionType
        public void SelectObject(string name, string selectionType) =>
            SelectObject(name, selectionType, SelectionData.Default);

        public IDisposable TemporarySelectObjects(IEnumerable<SelectedObject> selectedObjects, SelectionData selectionData, bool updateUserInterface = false, bool updateUserInterfaceAfterDisposing = true)
        {
            SelectObjects(selectedObjects, selectionData, updateUserInterface);
            var disposable = new SuspendSelectionListDisposable(BaseObject, x => 
            {
                _ = _disposables.Remove(x);
                if(updateUserInterfaceAfterDisposing)
                    UpdateInterface();
            });
            
            _disposables.Add(disposable);
            return disposable;
        }

        public IDisposable TemporarySelectObjects(IEnumerable<SelectedObject> selectedObjects, bool updateUserInterface = true)
            => TemporarySelectObjects(selectedObjects, SelectionData.Default, updateUserInterface);

        public void DeselectAll()
        {
            /// By some reason cant deselect all
            /// It not returns 1 or 0 that indicates success or not result as written in doc (https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISelectionMgr~IDeSelect2.html?verRedirect=1)
            /// It returns count of deselected items
            /// We call it until count reaches 0
            /// DeSelect2 also not works correctly, it return 0 (unsuccessful) on last iterations
            //while (count != 0)
            //    // Slow work and on big model throw SolidWorks onto desktop screen (silent crush)
            //    count -= BaseObject.IDeSelect2(count, 1, 0);

            /// Model method seams work better
            /// Not clearly understand what flag do, doc says:
            /// "False only works if the current PropertyManager page contains a selection list; otherwise, this method clears all selections."
            _model.UnsafeObject.ClearSelection2(true); 
        }

        /// <summary>
        /// Deselects element at specified index 
        /// </summary>
        /// <param name="index">Zero based index</param>
        /// <param name="mark"></param>
        public void DeselectAt(int index, int mark = -1) => BaseObject.DeSelect2(index + 1, mark);

        public override void Dispose()
        {
            _disposables.DisposeEach();
            base.Dispose();
        }

        private static void UpdateInterface()
        {
            SolidWorksEnvironment.Application.ActiveModel.UnsafeObject.GraphicsRedraw();
            var featureManager = SolidWorksEnvironment.Application.ActiveModel.UnsafeObject.FeatureManager.ToDnaObject();
            featureManager.UnsafeObject.UpdateFeatureTree();
            featureManager.Dispose();
        }
        #endregion
    }
}

