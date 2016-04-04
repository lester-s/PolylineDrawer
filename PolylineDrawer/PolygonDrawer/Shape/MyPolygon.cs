using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PolygonDrawer.MathSolver;
namespace PolygonDrawer.Shape
{
    /// <summary>
    /// This is a class to build poygon shapes. it implements the base interface IShape
    /// </summary>
    public class MyPolygon : IShape
    {
        public event Action<IShape> ShapeLeftMouseDown;

        public Gizmo ShapeGizmo { get; set; }

        public List<Point> Points { get; set; }

        public ShapeCollection<System.Windows.Shapes.Shape> Shapes { get; set; }

        public Polyline MyPolyline { get; set;}

        private CanvasHandler shapeCanvasHandler;
        public CanvasHandler ShapeCanvasHandler
        {
            get
            {
                return shapeCanvasHandler;
            }
            set
            {
                shapeCanvasHandler = value;
            }
        }

        public MyPolygon(CanvasHandler canvasHandler)
        {
            shapeCanvasHandler = canvasHandler;
            Points = new List<Point>();
            ShapeGizmo = new Gizmo();

            MyPolyline = new Polyline();
            MyPolyline.Fill = Brushes.CadetBlue;
            MyPolyline.FillRule = FillRule.Nonzero;
            MyPolyline.StrokeThickness = 4;
            shapeCanvasHandler.MyCanvas.Children.Add(MyPolyline);

            Shapes = new ShapeCollection<System.Windows.Shapes.Shape>();
            Shapes.Add(MyPolyline);
            Shapes.ItemMouseLeftDown += ItemMouseLeftDown;
            Shapes.ItemMouseOver += ItemMouseOver;
        }

        void ItemMouseOver(System.Windows.Shapes.Shape obj)
        {
            ShowCenter(obj);
        }

        private void ShowCenter(System.Windows.Shapes.Shape obj)
        {
            if (obj.IsMouseOver)
            {
                var centerPoint = Solver.GetApproximateCenter(Points);
                var width = 10;
                var height = 10;

                ShapeGizmo.Center = new Ellipse() { Width = width, Height = height };

                double left = centerPoint.X - (width / 2);
                double top = centerPoint.Y - (height / 2);

                ShapeGizmo.Center.Stroke = SystemColors.WindowFrameBrush;
                ShapeGizmo.Center.Margin = new Thickness(left, top, 1, 1);
                ShapeGizmo.Center.Fill = Brushes.Red;
                ShapeCanvasHandler.MyCanvas.Children.Add(ShapeGizmo.Center);
                ShapeCanvasHandler.CurrentPolygon.Shapes.Add(ShapeGizmo.Center);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = string.Format("Center: x={0} ; y={1}", centerPoint.X, centerPoint.Y);
                textBlock.Foreground = Brushes.Black;
                textBlock.Margin = new Thickness(centerPoint.X, centerPoint.Y, 0, 0);
                ShapeGizmo.Label = textBlock;
                ShapeCanvasHandler.MyCanvas.Children.Add(ShapeGizmo.Label);
            }
            else
            {
                ShapeCanvasHandler.MyCanvas.Children.Remove(ShapeGizmo.Center);
                ShapeCanvasHandler.MyCanvas.Children.Remove(ShapeGizmo.Label);
            }
        }

        void ItemMouseLeftDown(UIElement obj)
        {
            this.ShapeLeftMouseDown(this);
        }

        public bool ValidateShape()
        {
            if (Points.Count <= 3)
            {
                return true;
            }

            var pointCount = Points.Count;
            var lastLine = BuildOrderedLine(Points.ElementAt(pointCount - 1), Points.ElementAt(pointCount - 2));
            var lastFunction = Solver.GetLinearFunctionFromLine(lastLine);

            for (int i = 0; i <= Points.Count - 3; i++)
            {
                var currentLine = BuildOrderedLine(Points.ElementAt(i), Points.ElementAt(i + 1));
                var currentLineFunction = Solver.GetLinearFunctionFromLine(currentLine);
                var result = Solver.SolveIntersection(currentLineFunction, lastFunction, currentLine, lastLine);

                if (result.IsOnSegments && !Points.Contains(result.IntersectionPoint))
                {
                    return false;
                }
            }

            return true;
        }

        private Line BuildOrderedLine(Point point1, Point point2)
        {
            List<Point> p = new List<Point>() { point1, point2 };
            var ordered = p.OrderBy(t => t.X);

            var l = new Line() { X1 = ordered.ElementAt(0).X, Y1 = ordered.ElementAt(0).Y, X2 = ordered.ElementAt(1).X, Y2 = ordered.ElementAt(1).Y };
            return l;
        }

    }
}
