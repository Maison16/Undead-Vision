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
            var menuBackground = new ImageBrush();
            menuBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/menu/menuBackground.png"));
            mainCanvas.Background = menuBackground;


            var logo = new ImageBrush();
            logo.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/menu/biglogo.png"));

            Rectangle recLogo = new Rectangle
            {
                Width = 1920,
                Height = 750,
                Fill = logo
            };
            Canvas.SetLeft(recLogo, 0);
            Canvas.SetTop(recLogo, -100);
            mainCanvas.Children.Add(recLogo);
        }

        private void gotoGame(object sender, RoutedEventArgs e)
        {
            GameWindow Game = new GameWindow();
            Game.Show();
            this.Close();
        }
        private void gotoManual(object sender, RoutedEventArgs e)
        {
            Manual manual = new Manual();
            manual.Show();
            this.Close();
        }
        private void gotoCreators(object sender, RoutedEventArgs e)
        {
            Creators creators= new Creators();
            creators.Show();
            this.Close();
        }
        private void gotoExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}