using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace PolygonDrawer.Shape
{
    /// <summary>
    /// A base interface to define what a shape must have as a minimum properties and methods
    /// </summary>
    public interface IShape
    {
        List<Point> Points { get; set; }

        Gizmo ShapeGizmo { get; set; }

        ShapeCollection<System.Windows.Shapes.Shape> Shapes { get; set; }

        Polyline MyPolyline { get; set; }

        CanvasHandler ShapeCanvasHandler { get; set; }

        event Action<IShape> ShapeLeftMouseDown;

        bool ValidateShape();
    }
}
