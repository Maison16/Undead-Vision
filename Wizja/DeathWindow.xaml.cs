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
    /// Logika interakcji dla klasy DeathWindow.xaml
    /// </summary>
    public partial class DeathWindow : Window
    {
        public DeathWindow()
        {
            InitializeComponent();
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            MainWindow backToMenu = new MainWindow();
            backToMenu.Show();
            this.Close();
        }
    }
}
