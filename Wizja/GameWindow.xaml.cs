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
                    spawner.MoveEveryOne(player);
                });
            }
            catch { }
        }
        private void testing_ERYK()
        {
            player = new Player(gameCanvas);
            List<Point> enemiesSpawner = new List<Point>() { new Point(4000, 1500), new Point(4000, 2500), new Point(2000, 1500), new Point(2000, 2500) };
            //Spawner(List<Point> enemiesSpawner, int rounds,int betweenRounds,Canvas gameScreen)
            spawner = new Spawner(enemiesSpawner, 5, 1000, gameCanvas,player);
            int[][] enemyLists = new int[5][];
            enemyLists[0] = new int[] { 75, 25, 0, 0};
            enemyLists[1] = new int[] { 60, 35, 15, 0 };
            enemyLists[2] = new int[] { 40, 35, 25, 0 };
            enemyLists[3] = new int[] { 25, 35, 25, 5 };
            enemyLists[4] = new int[] { 10, 35, 25, 20 };
            spawner.GenerateEnemies(enemyLists[0], 10, 0, 105);
            spawner.GenerateEnemies(enemyLists[1], 20, 1, 85);
            spawner.GenerateEnemies(enemyLists[2], 40, 2, 75);
            spawner.GenerateEnemies(enemyLists[3], 60, 3, 65);
            spawner.GenerateEnemies(enemyLists[4], 100, 4, 55);
        }
    }
}
