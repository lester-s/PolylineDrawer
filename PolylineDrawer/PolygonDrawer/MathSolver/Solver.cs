using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace PolygonDrawer.MathSolver
{
    /// <summary>
    /// This is a math librairy that implement differents static method to calculate line intersection
    /// </summary>
    public class Solver
    {
        public static LinearFunction GetLinearFunctionFromLine(Line line)
        {
            LinearFunction result = new LinearFunction();
            result.Coef = ((line.Y2 - line.Y1) / (line.X2 - line.X1));
            result.Origine = line.Y1 - (result.Coef * line.X1);
            return result;
        }

        /// <summary>
        /// this method tell if two lines or crossing or not.
        /// https://fr.wikipedia.org/wiki/Intersection_(math%C3%A9matiques)
        /// i used the article to remind me the basics of equation solving.
        /// </summary>
        /// <returns></returns>
        public static IntersectionResult SolveIntersection(LinearFunction f1, LinearFunction f2, Line line1, Line line2)
        {
            var coefSub = f1.Coef - f2.Coef;
            var origineSub = f2.Origine - f1.Origine;
            var intersectionX = origineSub / coefSub;
            var intersectionY = (f1.Coef * intersectionX) + f1.Origine;

            var isOnLine1 = Solver.GetDeterminant(new Point(line1.X1, line1.Y1), new Point(line1.X2, line1.Y2), new Point(intersectionX, intersectionY)) <= 0;
            var isOnLine2 = Solver.GetDeterminant(new Point(line2.X1, line2.Y1), new Point(line2.X2, line2.Y2), new Point(intersectionX, intersectionY)) <= 0;

            IntersectionResult result = new IntersectionResult() { IsOnSegments = isOnLine1 && isOnLine2, IntersectionPoint = new Point(intersectionX, intersectionY) };
            return result;
        }

        /// <summary>
        /// This method tells you if a point is between two others.
        /// http://www.developpez.net/forums/d792420/general-developpement/algorithme-mathematiques/mathematiques/point-appartenant-segment/
        /// I used the comment #2 to know if a point is on a segment
        /// </summary>
        public static double GetDeterminant(Point segmentStart, Point segmentEnd, Point intersection)
        {
            var part1 = new Point(segmentStart.X - intersection.X, segmentStart.Y - intersection.Y);
            var part2 = new Point(segmentEnd.X - intersection.X, segmentEnd.Y - intersection.Y);
            return (part1.X * part2.X) + (part1.Y * part2.Y);
        }

        public static Point GetApproximateCenter(List<Point> points)
        {
            var xSum = points.Sum( p => p.X) / points.Count;
            var YSum = points.Sum(p => p.Y) / points.Count;

            return new Point(xSum, YSum);
        }
    }
}
