using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna.SketchManager;

public class SketchManager(global::SolidWorks.Interop.sldworks.SketchManager comObject)
    : SolidDnaObject<global::SolidWorks.Interop.sldworks.SketchManager>(comObject)
{
    public SketchPoint CreatePoint(XYZ point)
    {
        UnsafeObject.InsertSketch(true);

        UnsafeObject.AddToDB = true;

        var sketchPoint = UnsafeObject.CreatePoint(
            point.X,
            point.Y,
            point.Z);

        UnsafeObject.AddToDB = false;

        UnsafeObject.InsertSketch(false);

        return sketchPoint;
    }
}