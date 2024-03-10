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
using System.Windows.Shapes;

namespace Wizja
{
    /// <summary>
    /// Logika interakcji dla klasy Manual.xaml
    /// </summary>
    public partial class Manual : Window
    {
        public Manual()
        {
            InitializeComponent();
            var menuBackground = new ImageBrush();
            menuBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/menu/menuBackground.png"));
            manualCanvas.Background = menuBackground;

            var manual = new ImageBrush();
            manual.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/menu/bigmanual.png"));

            Rectangle image = new Rectangle
            {
                Width = 1365,
                Height = 475,
                Fill = manual
            };
            Canvas.SetLeft(image, 277);
            Canvas.SetTop(image, 200);
            manualCanvas.Children.Add(image);
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            MainWindow backToMenu = new MainWindow();
            backToMenu.Show();
            this.Close();
        }
    }
}
