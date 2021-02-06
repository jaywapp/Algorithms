using System;
using System.Windows;

namespace Algorithms.Helper
{
    public static class VectorHelper
    {
        public static double GetDegree(this Vector vector) => Math.Atan(vector.Y / vector.X);
    }
}
