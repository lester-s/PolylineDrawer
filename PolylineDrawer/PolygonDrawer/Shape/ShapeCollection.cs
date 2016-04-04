using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PolygonDrawer.Shape
{
    /// <summary>
    /// This is a collection to contain the visual shape and the polygons. It add events to it so
    /// we can interact with it on left mouse down and mouse over.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ShapeCollection<T>: Collection<T> 
    {
        public event Action<T> ItemMouseLeftDown;
        public event Action<T> ItemMouseOver;
        public event Action<IShape> ShapeMouseLeftDown;

        protected override void InsertItem(int index, T item)
        {
            if (item is UIElement)
            {
                (item as UIElement).MouseLeftButtonDown += item_MouseLeftButtonDown;
                (item as UIElement).IsMouseDirectlyOverChanged += IsMouseDirectlyOverChanged;
            }

            if (item is IShape)
            {
                (item as IShape).ShapeLeftMouseDown += ShapeLeftMouseDown;
            }
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            var item = this.ElementAt(index);

            if (item is UIElement)
            {
                (item as UIElement).MouseLeftButtonDown -= item_MouseLeftButtonDown;
                (item as UIElement).IsMouseDirectlyOverChanged -= IsMouseDirectlyOverChanged;
            }

            if (item is IShape)
            {
                (item as IShape).ShapeLeftMouseDown -= ShapeLeftMouseDown;
            }

            base.RemoveItem(index);
        }

        void IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.ItemMouseOver((T)sender);
        }

        void ShapeLeftMouseDown(IShape obj)
        {
            this.ShapeMouseLeftDown(obj);
        }

        void item_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.ItemMouseLeftDown((T)sender);
        }
    }
}
