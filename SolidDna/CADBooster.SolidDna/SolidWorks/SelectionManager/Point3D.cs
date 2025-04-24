namespace CADBooster.SolidDna
{
    public struct Point3D
    {
        /// <summary>
        /// Gets or sets the X-coordinate of the point
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y-coordinate of the point
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Z-coordinate of the point
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// A static readonly instance representing the origin point (0, 0, 0)
        /// </summary>
        public static readonly Point3D Zero = new Point3D(0, 0, 0);

        /// <summary>
        /// Initializes a new instance of the Point3D struct with specified coordinates
        /// </summary>
        /// <param name="x">The X-coordinate value</param>
        /// <param name="y">The Y-coordinate value</param>
        /// <param name="z">The Z-coordinate value</param>
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Returns a string representation of the point in the format: "Point3D { X = [value], Y = [value], Z = [value] }"
        /// </summary>
        /// <returns>A string representation of the point</returns>
        public override string ToString() => $"Person {{ X = {X}, Y = {Y}, Z = {Z}}}";
    }
}

