using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Wizja.classes;

namespace Wizja
{
    /// <summary>
    /// Logika interakcji dla klasy GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        //Testy przeciwników
        public Spawner spawner;
        public Player player;



        private System.Timers.Timer gameTimer = new System.Timers.Timer();
        Point playerPosition; // testy
        ObjectLoader objectLoader;
        public GameWindow()
        {
            InitializeComponent();


            //ładowanie mapy
            objectLoader = new ObjectLoader(gameCanvas);
            //dodawanie obiektu hudu
            HUD hud = new HUD(100, 30, 300, statCanvas);

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
            Canvas.SetLeft(playerRect, 3100);
            Canvas.SetTop(playerRect, 2100);
            gameCanvas.Children.Add(playerRect);

            playerPosition = new Point(Canvas.GetLeft(playerRect), Canvas.GetTop(playerRect));
            PreviewKeyDown += GameWindow_PreviewKeyDown;
            testing_ERYK();


            //inicjalizacja gameTimera
            gameTimer.Interval = 16; //16 MILISEKUND
            gameTimer.Elapsed += GameTick;
            gameTimer.Start(); //od razu zaczyna timer gameTimer



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

                    //Testowanie Przeciwników
                    spawner.Spawn();
                    spawner.MoveEveryOne(player.playerImage);
                });
            }
            catch { }
        }
        private void testing_ERYK()
        {
            player = new Player(gameCanvas);
            List<Point> enemiesSpawner = new List<Point>() { new Point(1500, 1000), new Point(1000, 1500), new Point(2000, 1500), new Point(1500, 2000) };
            //    Spawner(int frequency, List<Point> enemiesSpawner, int rounds,int betweenRounds,Canvas gameScreen)
            spawner = new Spawner(20, enemiesSpawner, 5, 200, gameCanvas);
            List<int>[] enemyLists = new List<int>[5];
            enemyLists[0] = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 };
            enemyLists[1] = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 };
            enemyLists[2] = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 };
            enemyLists[3] = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 };
            enemyLists[4] = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 };
            spawner.GetEnemies(enemyLists);
        }
    }
}
