using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Wizja.Enemies;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Wizja.classes
{
    public class SlowZombie : Enemy
    {
        public int healthPoints;
        public int damagePoints;
        public int value; // Wartość pięniedzy które opuszcza po śmierci
        private double movingSpeed;
        public Rectangle enemyImage;
        public bool isLiving = false; // True jężeli przeciwnik żyje oraz jest na mapie
        private int coolDown = 62; // Co ileś ticków zadaje obrażenia
        private int tickCount = 0;
        static private Player player;
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/FastZombie.png"));
        public static ImageSource sourceLF1 = new BitmapImage(new Uri("pack://application:,,,/res/FastZombieLF1.png"));
        public static ImageSource sourceLF2 = new BitmapImage(new Uri("pack://application:,,,/res/FastZombieLF2.png"));
        public static ImageSource sourceRF1 = new BitmapImage(new Uri("pack://application:,,,/res/FastZombieRF1.png"));
        public static ImageSource sourceRF2 = new BitmapImage(new Uri("pack://application:,,,/res/FastZombieRF2.png"));
        //    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)
        public SlowZombie() : base(5, 10, 10, 2, source,64,64)
        { }
        int i = 0;
        int j = 0;
        public void Follow(Rectangle playerLocation, Canvas gameScreen)
        {
            i++;
            if (i == 64)
            {
                j++;
                switch (j)
                {
                    case 0:
                        enemyImage.Fill = new ImageBrush(source);
                        break;
                    case 1:
                        enemyImage.Fill = new ImageBrush(sourceLF1);
                        break;
                    case 2:
                        enemyImage.Fill = new ImageBrush(sourceLF2);
                        break;
                    case 3:
                        enemyImage.Fill = new ImageBrush(sourceLF1);
                        break;
                    case 4:
                        enemyImage.Fill = new ImageBrush(source);
                        break;
                    case 5:
                        enemyImage.Fill = new ImageBrush(sourceRF1);
                        break;
                    case 6:
                        enemyImage.Fill = new ImageBrush(sourceRF2);
                        break;
                    case 7:
                        enemyImage.Fill = new ImageBrush(sourceRF1);
                        j = 0;
                        break;
                }
                i = 0;
            }
            Random rnd = new Random();
            double x = Canvas.GetLeft(enemyImage);
            double y = Canvas.GetTop(enemyImage);
            double dx = Canvas.GetLeft(playerLocation) - x;
            double dy = Canvas.GetTop(playerLocation) - y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            double dirX;
            double dirY;
            if (distance >= 40 && distance <= 120) // Jeżeli player jest dość blisko staraj podążać się obok niego 
            {
                dirX = (dx + MinusOrPlus() * rnd.Next(16)) / distance;
                dirY = (dy + MinusOrPlus() * rnd.Next(16)) / distance;
                x += dirX * movingSpeed;
                y += dirY * movingSpeed;
                Canvas.SetLeft(enemyImage, x);
                Canvas.SetTop(enemyImage, y);
            }
            else if (distance >= 40) // Jeżeli player jest daleko Losuj bardziej jego scieżke
            {
                dirX = (dx + MinusOrPlus() * rnd.Next(24, 48)) / distance;
                dirY = (dy + MinusOrPlus() * rnd.Next(24, 48)) / distance;
                x += dirX * movingSpeed;
                y += dirY * movingSpeed;
                Canvas.SetLeft(enemyImage, x);
                Canvas.SetTop(enemyImage, y);
            }
        }
    }
}