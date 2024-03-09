using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using Wizja.classes.guns;
using Wizja.Enemies;
using static Wizja.classes.Shop;
namespace Wizja.classes
{
    public class Player
    {
        public int healthPoints;
        public double movingSpeed;
        public Rectangle playerImage;
        public Rectangle flashLightImage;
        private List<Rectangle> obstacles;
        private List<Enemy> allEnemies;
        private Weapon weapon;
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/Player.png"));
        public static ImageSource flashLightSource = new BitmapImage(new Uri("pack://application:,,,/res/flashlight.png"));
        public HUD hud;

        public void setWeapon(Weapon w)
        {
            this.weapon = w;
        }
        public Player(Canvas gameCanvas, HUD hud, List<Rectangle> obstacles)


        {
            healthPoints = 100;
            movingSpeed = 20;
            this.hud = hud;
            this.obstacles = obstacles;
            this.allEnemies = allEnemies;

            playerImage = new Rectangle()
            {
                Width = 47,
                Height = 75,
                Fill = new ImageBrush(source)
            };
            flashLightImage = new Rectangle()
            {
                Width = 3000,
                Height = 3000,
                Fill = new ImageBrush(flashLightSource),
                Opacity = 0.95
            };
            Panel.SetZIndex(flashLightImage, int.MaxValue);
            Canvas.SetLeft(playerImage, 3000);
            Canvas.SetTop(playerImage, 2000);
            Canvas.SetLeft(flashLightImage, 1523.5);
            Canvas.SetTop(flashLightImage, 537.5);
            gameCanvas.Children.Add(playerImage);
            gameCanvas.Children.Add(flashLightImage);
            playerImage.RenderTransformOrigin = new Point(0.5, 0.5);

            this.weapon = new BaseGun();
        }
        public void TakeDamage(int damage)
        {
            healthPoints = hud.GetHp();
            healthPoints -= damage;
            if (healthPoints <= 0)
            {
                hud.SetHp(0);
            }
            else
            {
                hud.SetHp(healthPoints);
            }
        }
        Canvas gameCanvas;
        public void MouseMoveHandler(Canvas gameCanvas)
        {
            this.gameCanvas = gameCanvas;
            gameCanvas.MouseMove += MouseMove;
            gameCanvas.MouseLeftButtonDown += MouseLeftButtonDown;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(gameCanvas);
            Point playerPosition = new Point(Canvas.GetLeft(flashLightImage) + flashLightImage.Width / 2, Canvas.GetTop(flashLightImage) + flashLightImage.Height / 2);
            double angle = Math.Atan2(mousePosition.Y - playerPosition.Y, mousePosition.X - playerPosition.X) * (180 / Math.PI);
            RotateDarknes(angle);
        }
        private void RotateDarknes(double angle)
        {
            flashLightImage.RenderTransformOrigin = new Point(0.5, 0.5);
            RotateTransform rotateTransform = new RotateTransform(angle);
            flashLightImage.RenderTransform = rotateTransform;
        }

        public void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point endPoint;
            Point playerPos = new Point(Canvas.GetLeft(playerImage), Canvas.GetTop(playerImage));

            playerPos = new Point(Canvas.GetLeft(playerImage) + playerImage.Width / 2, Canvas.GetTop(playerImage) + playerImage.Height / 2);

            // pojebana matma z chatu do endpointu strzalu
            double flashlightAngle = ((RotateTransform)flashLightImage.RenderTransform).Angle;
            Vector direction = new Vector(Math.Cos(flashlightAngle * Math.PI / 180), Math.Sin(flashlightAngle * Math.PI / 180));
            // double distance = 1000;
            // endPoint = new Point(playerPos.X + direction.X * distance, playerPos.Y + direction.Y * distance);

            weapon.Shoot(playerPos, direction, obstacles, allEnemies, gameCanvas);
        }

        public void SetAllEnemies(List<Enemy> allEnemies)
        {
            this.allEnemies = allEnemies;
        }
    }
}
