using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PolygonDrawer.MathSolver
{
    /// <summary>
    /// this class allow to know where is the intersection point is or might be located 
    /// regarding if the lines or actually crossing or not .
    /// </summary>
    public class IntersectionResult
    {
        public Point IntersectionPoint { get; set; }
        public bool IsOnSegments { get; set; }
    }
}
