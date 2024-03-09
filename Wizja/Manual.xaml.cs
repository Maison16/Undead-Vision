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
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            MainWindow backToMenu = new MainWindow();
            backToMenu.Show();
            this.Close();
        }
    }
}
