using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADBooster.SolidDna.SolidWorks.Models.Views;

public class ModelView(IModelView comObject, Model model) : SolidDnaObject<IModelView>(comObject)
{
    public ModelView SetOrientation(XYZ viewDirection, XYZ upDirection)
    {
        var mathUtil = SolidWorksEnvironment.MathUtility;

        var z = viewDirection;
        var y = upDirection;
        var x = y.CrossProduct(z);

        y = z.CrossProduct(x);

        var origin = XYZ.Zero;

        var transform = (MathTransform)mathUtil.ComposeTransform(
            x.AsMathVector(),
            y.AsMathVector(),
            z.AsMathVector(),
            origin.AsMathVector(),
            1.0).Inverse();

        UnsafeObject.Orientation3 = transform;

        UnsafeObject.Translation3 = origin.AsMathVector();

        return this;
    }
}