using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wizja.classes;

namespace Wizja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void gotoGame(object sender, RoutedEventArgs e)
        {
            GameWindow Game = new GameWindow();
            Game.Show();
            this.Close();
        }
        private void gotoExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}