using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonDrawer.Tools
{
    /// <summary>
    /// Erasing tool
    /// </summary>
    public class Eraser : ITool
    {
        private CanvasHandler toolCanvas;
        public CanvasHandler ToolCanvas
        {
            get { return toolCanvas; }
            set { toolCanvas = value; }
        }

        public Eraser(ITool tool)
        {
            ToolCanvas = tool.ToolCanvas;
        }

        public Eraser(CanvasHandler canvasHandler)
        {
            ToolCanvas = canvasHandler;
        }

        public void ExecuteMainAction(Shape.IShape shapeToDestroy)
        {
            foreach (var shape in shapeToDestroy.Shapes)
            {
                ToolCanvas.MyCanvas.Children.Remove(shape);
            }

            var index = ToolCanvas.Polygons.IndexOf(shapeToDestroy);
            if (index == ToolCanvas.Polygons.Count - 1)
            {
                ToolCanvas.Polygons.Add(new Shape.MyPolygon(ToolCanvas));
                ToolCanvas.CurrentPolygon = ToolCanvas.Polygons.ElementAt(ToolCanvas.Polygons.Count - 1);
            }

            ToolCanvas.Polygons.Remove(shapeToDestroy);
        }

        public void ExecuteMainAction(System.Windows.Point point){}
    }
}
