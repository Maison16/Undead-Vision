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
namespace Wizja.classes
{
    public class Player
    {
        public int healthPoints;
        public double movingSpeed;
        public Rectangle playerImage;
        public Rectangle flashLightImage;
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/Player.png"));
        public static ImageSource flashLightSource = new BitmapImage(new Uri("pack://application:,,,/res/flashlight.png"));
        HUD hud;
        public Player(Canvas gameCanvas, HUD hud)
        {
            healthPoints = 100;
            movingSpeed = 3.5;
            this.hud = hud;
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
    }
}
