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
using Wizja.classes;

namespace Wizja
{
    /// <summary>
    /// Logika interakcji dla klasy DeathWindow.xaml
    /// </summary>
    public partial class DeathWindow : Window
    {
        HUD hud;
        public DeathWindow(HUD hud)
        {
            this.hud = hud;
            InitializeComponent();
            Label score = new Label();
            score.Content = "Score: " + hud.totalPoints.ToString();
            score.FontSize = 36;
            score.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(score, 920);
            Canvas.SetTop(score, 350);
            deathCanvas.Children.Add(score);
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            MainWindow backToMenu = new MainWindow();
            backToMenu.Show();
            this.Close();
        }
    }
}
