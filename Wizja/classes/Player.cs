using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/Player.png"));
        HUD hud;
        public Player(Canvas gameCanvas,HUD hud) 
        {
            healthPoints = 100;
            movingSpeed = 3.5;
            this.hud = hud;
            playerImage = new Rectangle()
            {
                Width = 64,
                Height = 64,
                Fill = new ImageBrush(source)
            };
            Canvas.SetLeft(playerImage, 3000);
            Canvas.SetTop(playerImage,  2000);
            gameCanvas.Children.Add(playerImage);
            playerImage.RenderTransformOrigin = new Point(0.5, 0.5);
        }
        public void TakeDamage(int damage) 
        {
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
    }
}
