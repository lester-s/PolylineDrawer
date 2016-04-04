using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PolygonDrawer.Shape
{
    /// <summary>
    /// The class that is used to build the visual effect that locate a shape on the canvas
    /// </summary>
    public class Gizmo
    {
        public Ellipse Center { get; set; }
        public TextBlock Label { get; set; }
    }
}
