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
    /// Logika interakcji dla klasy Creators.xaml
    /// </summary>
    public partial class Creators : Window
    {
        public Creators()
        {
            InitializeComponent();
            var menuBackground = new ImageBrush();
            menuBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/menu/menuBackground.png"));
            creatorsCanvas.Background = menuBackground;

            var creators = new ImageBrush();
            creators.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/menu/creator.png"));

            Rectangle image = new Rectangle
            {
                Width = 1141,
                Height = 583,
                Fill = creators
            };
            Canvas.SetLeft(image, 389);
            Canvas.SetTop(image,  100);
           creatorsCanvas.Children.Add(image);
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            MainWindow backToMenu = new MainWindow();
            backToMenu.Show();
            this.Close();
        }
    }
}
