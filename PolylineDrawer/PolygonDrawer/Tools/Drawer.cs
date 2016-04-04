using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using PolygonDrawer.Shape;

namespace PolygonDrawer.Tools
{
    /// <summary>
    /// drawing tool
    /// http://www.c-sharpcorner.com/uploadfile/mahesh/polyline-in-wpf/
    /// I used this tutorial for the basics of canvas drawing
    /// </summary>
    public class Drawer: ITool
    {
        private static int maxDistanceToClose = 15;

        private CanvasHandler toolCanvas;
        public CanvasHandler ToolCanvas
        {
            get { return toolCanvas; }
            set { toolCanvas = value; }
        }

        public Drawer(ITool tool)
        {
            ToolCanvas = tool.ToolCanvas;
        }

        public Drawer(CanvasHandler canvasHandler)
        {
            ToolCanvas = canvasHandler;
        }

        /// <summary>
        /// first step for the drawing of new point
        /// </summary>
        /// <param name="point"></param>
        public void ExecuteMainAction(Point point)
        {
            var isClosing = this.IsClosingPolygon(point);

            ToolCanvas.CurrentPolygon.Points.Add(point);
            ToolCanvas.CurrentPolygon.MyPolyline.Points.Add(point);

            var isShapeOk = ToolCanvas.CurrentPolygon.ValidateShape();

            if (!isShapeOk || isClosing)
            {
                ToolCanvas.CurrentPolygon.Points.Remove(point);
                ToolCanvas.CurrentPolygon.MyPolyline.Points.Remove(point);
                if (!isShapeOk)
                {
                    return;
                }
            }

            if (isClosing)
            {
                ClosePolygon();
            }
            else if(!isClosing)
            {
                if (ToolCanvas.CurrentPolygon.Points.Count > 1)
                {
                    SetLinePoint();
                }
                else
                {
                    DrawCircle(point);
                }
            }
        }

        /// <summary>
        /// check if you are trying to close your shape
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool IsClosingPolygon(Point point)
        {
            if (ToolCanvas.CurrentPolygon.Points.Count <= 2)
            {
                return false;
            }

            return CalculateDistanceToStartPoint(point) <= maxDistanceToClose;
        }

        private void SetLinePoint()
        {
            var pointCount = ToolCanvas.CurrentPolygon.Points.Count;
            var startPoint = ToolCanvas.CurrentPolygon.Points.ElementAt(pointCount - 2);
            var endPoint = ToolCanvas.CurrentPolygon.Points.ElementAt(pointCount - 1);
            DrawCircle(endPoint);
            DrawLine(startPoint, endPoint);
        }

        private void DrawLine(Point startPoint, Point endPoint)
        {
            Line line = new Line();

            line.Stroke = SystemColors.WindowFrameBrush;
            line.StrokeThickness = 5;
            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = endPoint.X;
            line.Y2 = endPoint.Y;

            ToolCanvas.MyCanvas.Children.Add(line);
            ToolCanvas.CurrentPolygon.Shapes.Add(line);
        }

        private void DrawCircle(Point currentPoint)
        {
            var width = maxDistanceToClose;
            var height = maxDistanceToClose;

            Ellipse circle = new Ellipse() { Width = width, Height = height };

            double left = currentPoint.X - (width / 2);
            double top = currentPoint.Y - (height / 2);

            circle.Stroke = SystemColors.WindowFrameBrush;
            circle.Margin = new Thickness(left, top, 0, 0);
            circle.Fill = Brushes.Blue;
            ToolCanvas.MyCanvas.Children.Add(circle);
            ToolCanvas.CurrentPolygon.Shapes.Add(circle);
        }

        private void ClosePolygon()
        {
            DrawLine(ToolCanvas.CurrentPolygon.Points.ElementAt(ToolCanvas.CurrentPolygon.Points.Count - 1), ToolCanvas.CurrentPolygon.Points.ElementAt(0));
            ToolCanvas.Polygons.Add(new Shape.MyPolygon(ToolCanvas));
            ToolCanvas.CurrentPolygon = ToolCanvas.Polygons.ElementAt(ToolCanvas.Polygons.Count - 1);
        }

        /// <summary>
        /// calculate the distance between your last point and the first point.
        /// </summary>
        /// <param name="currentPoint"></param>
        /// <returns></returns>
        private double CalculateDistanceToStartPoint(Point currentPoint)
        {
            var startPoint = ToolCanvas.CurrentPolygon.Points.ElementAt(0);

            var x = Math.Pow(startPoint.X - currentPoint.X, 2);

            var y = Math.Pow(startPoint.Y - currentPoint.Y, 2);

            var distance = Math.Sqrt(x + y);

            return distance;

        }

        public void ExecuteMainAction(Shape.IShape shape)
        {
            
        }
    }
}
