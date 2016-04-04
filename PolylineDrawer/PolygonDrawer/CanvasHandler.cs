using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PolygonDrawer.Shape;
using PolygonDrawer.Tools;

namespace PolygonDrawer
{
    /// <summary>
    /// This class is handling all the drawing.
    /// </summary>
    public class CanvasHandler
    {
        public ShapeCollection<IShape> Polygons { get; set; }
        public List<ITool> Tools { get; set; }
        private ITool CurrentTool { get; set; }
        public IShape CurrentPolygon { get; set; }
        public Canvas MyCanvas { get; set; }

        public CanvasHandler(Canvas canvas)
        {
            MyCanvas = canvas;
            Polygons = new ShapeCollection<IShape>();
            Polygons.ShapeMouseLeftDown += ShapeMouseLeftDown;

            Tools = new List<ITool>();
            Tools.Add(new Drawer(this));
            Tools.Add(new Eraser(this));

            Polygons.Add(new MyPolygon(this));
            CurrentPolygon = Polygons.ElementAt(0) as MyPolygon;

            CurrentTool = Tools.First(t => t.GetType() == typeof(Drawer));
        }

        void ShapeMouseLeftDown(IShape obj)
        {
            CurrentTool.ExecuteMainAction(obj);
        }

        public void ExecuteToolAction(Point point)
        {
            CurrentTool.ExecuteMainAction(point);
        }

        internal void ChangeTool<T>() where T: ITool
        {
            CurrentTool = Tools.First(t => t.GetType() == typeof(T));
        }
    }
}
