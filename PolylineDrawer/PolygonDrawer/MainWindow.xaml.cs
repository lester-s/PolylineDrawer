using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PolygonDrawer.Tools;

namespace PolygonDrawer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static CanvasHandler canvasHandler;
        public MainWindow()
        {
            InitializeComponent();
            canvasHandler = new CanvasHandler(MyCanvas);
        }

        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvasHandler.ExecuteToolAction(e.GetPosition(this.MyCanvas));
        }

        private void DrawerToolButton_Click(object sender, RoutedEventArgs e)
        {
            canvasHandler.ChangeTool<Drawer>();
        }

        private void EraserToolButton_Click(object sender, RoutedEventArgs e)
        {
            canvasHandler.ChangeTool<Eraser>();
        }
    }
}
