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
    /// Logika interakcji dla klasy GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Point playerPosition;
        private Rectangle enemy;
        public GameWindow()
        {
            InitializeComponent();

            // TESTY TESCIKI //
            // test kloc jako player
            Rectangle playerRect = new Rectangle
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Blue
            };
            Canvas.SetLeft(playerRect, 960);
            Canvas.SetTop(playerRect, 512);
            gameCanvas.Children.Add(playerRect);
            playerPosition = new Point(Canvas.GetLeft(playerRect), Canvas.GetTop(playerRect));

            PreviewKeyDown += GameWindow_PreviewKeyDown;


            // test przeciwnik
            enemy = new Rectangle
            {
                Width = 35,
                Height = 35,
                Fill = Brushes.Black
            };
            Canvas.SetLeft(enemy, 860);
            Canvas.SetTop(enemy, 512);
            gameCanvas.Children.Add(enemy);
        }

        private void GameWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                ShootProjectile();
            }
        }

        private void ShootProjectile()
        {
            // pozycja kursora
            Point cursorPosition = Mouse.GetPosition(gameCanvas);

            // wektor pocisku
            Vector direction = cursorPosition - playerPosition;
            direction.Normalize();

            // koncowy punkt 
            Point endPoint = playerPosition + direction * 500; // 500 to range do zmiany 

            // Stworz pocisk
            Projectile projectile = new Projectile(playerPosition.X, playerPosition.Y, endPoint.X, endPoint.Y, 2, enemy, gameCanvas);
        }
    }
}
