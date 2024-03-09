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
using System.Timers;

namespace Wizja
{
    /// <summary>
    /// Logika interakcji dla klasy GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private System.Timers.Timer gameTimer= new System.Timers.Timer();
        Point playerPosition; // testy
        ObjectLoader objectLoader;
        public GameWindow()
        {
            InitializeComponent();
            //inicjalizacja gameTimera
            gameTimer.Interval = 16; //16 MILISEKUND
            gameTimer.Elapsed += GameTick;
            gameTimer.Start(); //od razu zaczyna timer gameTimer


            //ładowanie mapy
            objectLoader = new ObjectLoader(gameCanvas);
            //dodawanie obiektu hudu
            HUD hud= new HUD(100, 30, 300, statCanvas);

            //Tworzenie i otwieranie shop do testów
            Shop itemshop = new Shop(shopCanvas, hud);
            itemshop.ShowShop();

            // jakis test ziom do strzelania
            Rectangle playerRect = new Rectangle
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Blue
            };
            Canvas.SetLeft(playerRect, 3000);
            Canvas.SetTop(playerRect, 2000);
            gameCanvas.Children.Add(playerRect);

            playerPosition = new Point(Canvas.GetLeft(playerRect), Canvas.GetTop(playerRect));
            PreviewKeyDown += GameWindow_PreviewKeyDown;
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
            Projectile projectile = new Projectile(playerPosition.X, playerPosition.Y, endPoint.X, endPoint.Y, 2, objectLoader.GetListMapObjects(), gameCanvas);
        }

        // cosik do klikania 
        private void GameWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                ShootProjectile();
            }
        }

        private void GameTick(object sender, ElapsedEventArgs e)
        {
            try
            {
                gameCanvas.Dispatcher.Invoke(() =>
                {
                    //tutaj pisz SEBA
                    /*w object loader masz:
                    public List<Rectangle> GetListMovingObjects()
                    {
                        return movingObjects;
                    }
                    więc obiekt objectLoader ma na liście wszystkie elementy, które mają się poruszać.
                    */
                });
            }
            catch { }
        }
    }
}
