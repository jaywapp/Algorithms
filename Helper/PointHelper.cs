using System;
using System.Windows;

namespace Algorithms.Helper
{
    public static class PointHelper
    {
        public static double GetDistance(this Point pt1, Point pt2)
        {
            var xDistance = Math.Abs(pt1.X - pt2.X);
            var yDistance = Math.Abs(pt1.Y - pt2.Y);

            return Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
        }

        public static bool IsCCW(Point pt1, Point pt2, Point pt3)
        {
            return (pt1.X * pt2.Y + pt2.X * pt3.Y + pt3.X * pt1.Y) 
                - (pt2.X * pt1.Y + pt3.X * pt2.Y + pt1.X * pt3.Y) > 0;
        }

        public static double GetDegree(Point pt1, Point pt2) => (pt2 - pt1).GetDegree();
    }
}
