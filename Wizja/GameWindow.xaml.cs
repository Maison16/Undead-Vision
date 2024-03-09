using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Wizja.classes;
using Wizja.classes.guns;

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
        public HUD hud;


        private System.Timers.Timer gameTimer = new System.Timers.Timer();
        Point playerPosition; // testy
        ObjectLoader objectLoader;
        Weapon weapon; // testy
        public GameWindow()
        {
            InitializeComponent();


            //ładowanie mapy
            objectLoader = new ObjectLoader(gameCanvas);
            //dodawanie obiektu hudu
            hud = new HUD(100, 30, 300, statCanvas);

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
            
            // test pistoletu
            BaseGun gun = new BaseGun();
            weapon = gun;

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
            weapon.Shoot(playerPosition, endPoint, objectLoader, gameCanvas);
            //Projectile projectile = new Projectile(playerPosition.X, playerPosition.Y, endPoint.X, endPoint.Y, 2, objectLoader.GetListMapObjects(), gameCanvas);
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
                    EndOfGame();
                });
            }
            catch { }
        }
        private void testing_ERYK()
        {
            player = new Player(gameCanvas, hud);
            List<Point> enemiesSpawner = new List<Point>() { new Point(4000, 1500), new Point(4000, 2500), new Point(2000, 1500), new Point(2000, 2500) };
            int[][] enemyLists = new int[5][];
            //Spawner(List<Point> enemiesSpawner, int rounds,int betweenRounds,Canvas gameScreen)
/*            spawner = new Spawner(enemiesSpawner, 5, 1000, gameCanvas,player);
            enemyLists[0] = new int[] { 75, 25, 0, 0};
            enemyLists[1] = new int[] { 60, 35, 15, 0 };
            enemyLists[2] = new int[] { 40, 35, 25, 0 };
            enemyLists[3] = new int[] { 25, 35, 25, 5 };
            enemyLists[4] = new int[] { 10, 35, 25, 20 };
            spawner.GenerateEnemies(enemyLists[0], 12, 0, 125);
            spawner.GenerateEnemies(enemyLists[1], 24, 1, 115);
            spawner.GenerateEnemies(enemyLists[2], 36, 2, 110);
            spawner.GenerateEnemies(enemyLists[3], 48, 3, 105);
            spawner.GenerateEnemies(enemyLists[4], 60, 4, 100);*/
                     
            spawner = new Spawner(enemiesSpawner, 5, 100000, gameCanvas,player);
            enemyLists[0] = new int[] { 0, 100, 0, 0 };
            enemyLists[1] = new int[] { 60, 35, 15, 0 };
            enemyLists[2] = new int[] { 40, 35, 25, 0 };
            enemyLists[3] = new int[] { 25, 35, 25, 5 };
            enemyLists[4] = new int[] { 10, 35, 25, 20 };
            spawner.GenerateEnemies(enemyLists[0], 1, 0, 125);
            spawner.GenerateEnemies(enemyLists[1], 24, 1, 115);
            spawner.GenerateEnemies(enemyLists[2], 36, 2, 110);
            spawner.GenerateEnemies(enemyLists[3], 48, 3, 105);
            spawner.GenerateEnemies(enemyLists[4], 60, 4, 100);
        }

        private void EndOfGame() 
        {
            if (player.healthPoints == 0) 
            {
                DeathWindow deathWindow = new DeathWindow();
                gameTimer.Stop();
                deathWindow.Show();
                this.Close();
            }
        }
    }
}
