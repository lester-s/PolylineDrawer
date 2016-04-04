using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PolygonDrawer.Shape;

namespace PolygonDrawer.Tools
{
    /// <summary>
    /// This is the base interface for a tool. It must be implemented to create a new tool
    /// http://www.dofactory.com/net/state-design-pattern
    /// I used this link to well implement the state pattern.
    /// </summary>
    public interface ITool
    {
        void ExecuteMainAction(Point point);

        void ExecuteMainAction(IShape shape);

        CanvasHandler ToolCanvas { get; set; }
    }
}
