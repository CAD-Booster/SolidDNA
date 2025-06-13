using SolidWorks.Interop.sldworks;
using System;
using UnitsNet;
using UnitsNet.Units;

namespace CADBooster.SolidDna
{
    public class XYZ
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double[] ArrayData => new[] { X, Y, Z };

        public static XYZ BasisX => new XYZ(1, 0, 0);
        public static XYZ BasisY => new XYZ(0, 1, 0);
        public static XYZ BasisZ => new XYZ(0, 0, 1);
        public static XYZ Zero => new XYZ(0, 0, 0);

        public double this[int index] => ArrayData[index];


        public XYZ(MathPoint mathPoint)
        {
            var coords = (double[])mathPoint.ArrayData;

            X = coords[0];
            Y = coords[1];
            Z = coords[2];
        }
        public XYZ(MathVector mathVector)
        {
            var coords = (double[])mathVector.ArrayData;

            X = coords[0];
            Y = coords[1];
            Z = coords[2];
        }
        public XYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public static XYZ operator +(XYZ thisPoint, XYZ otherPoint)
        {
            return new XYZ(
                thisPoint.X + otherPoint.X,
                thisPoint.Y + otherPoint.Y,
                thisPoint.Z + otherPoint.Z);
        }

        public static XYZ operator -(XYZ thisPoint, XYZ otherPoint)
        {
            return new XYZ(
                thisPoint.X - otherPoint.X,
                thisPoint.Y - otherPoint.Y,
                thisPoint.Z - otherPoint.Z);
        }

        public static XYZ operator -(XYZ thisPoint)
        {
            return new XYZ(-thisPoint.X, -thisPoint.Y, -thisPoint.Z);
        }

        public static XYZ operator *(XYZ thisPoint, XYZ otherPoint)
        {
            return new XYZ(
                thisPoint.X * otherPoint.X,
                thisPoint.Y * otherPoint.Y,
                thisPoint.Z * otherPoint.Z);
        }

        public static XYZ operator *(XYZ thisPoint, double multiplier)
        {
            return new XYZ(
                 thisPoint.X * multiplier,
                 thisPoint.Y * multiplier,
                 thisPoint.Z * multiplier);
        }

        public static XYZ operator /(XYZ dividend, double divisor)
        {
            return new XYZ(
                dividend.X / divisor,
                dividend.Y / divisor,
                dividend.Z / divisor);
        }

        public MathPoint AsMathPoint()
        {
            return SolidWorksEnvironment.Application.MathUtility
                .CreatePoint(new[] { X, Y, Z }) as MathPoint;
        }

        public MathVector AsMathVector()
        {
            return SolidWorksEnvironment.Application.MathUtility
                .CreateVector(new[] { X, Y, Z }) as MathVector;
        }

        public static XYZ Convert(MathPoint mathPoint)
        {
            var coords = (double[])mathPoint.ArrayData;

            if (coords.Length != 3)
                throw new ArgumentException("MathPoint must have 3 coordinates.");

            return new XYZ(coords[0], coords[1], coords[2]);
        }

        public static XYZ Convert(MathVector mathVector)
        {
            var coords = (double[])mathVector.ArrayData;

            if (coords.Length != 3)
                throw new ArgumentException("MathVector must have 3 coordinates.");

            return new XYZ(coords[0], coords[1], coords[2]);
        }

        public XYZ Transform(MathTransform transform)
        {
            var mathPoint = AsMathPoint();

            var transformed = mathPoint.IMultiplyTransform(transform);

            return XYZ.Convert(transformed);
        }

        public XYZ MoveAlongVector(XYZ vector, double distance) 
            => this + (vector * distance);

        public XYZ CrossProduct(XYZ other)
        {
            return new XYZ(
                (Y * other.Z) - (Z * other.Y),
                (Z * other.X) - (X * other.Z),
                (X * other.Y) - (Y * other.X));
        }

        public XYZ Scale(double scale)
            => this * scale;

        public bool IsAlmostEquals(XYZ other, double tolerance = 1E-5)
        {
            return Math.Abs(X - other.X) < tolerance &&
                   Math.Abs(Y - other.Y) < tolerance &&
                   Math.Abs(Z - other.Z) < tolerance;
        }

        public XYZ Normalize() 
            => new(AsMathVector().Normalise());

        public XYZ VectorTo(XYZ end) => (end - this).Normalize();

        public XYZ Convert(LengthUnit from, LengthUnit to)
        {
            return new XYZ(
                Length.From(X, from).ToUnit(to).Value,
                Length.From(Y, from).ToUnit(to).Value,
                Length.From(Z, from).ToUnit(to).Value);
        }
    }
}
