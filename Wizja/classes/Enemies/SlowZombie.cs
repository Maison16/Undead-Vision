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
        static private Player player;
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/FastZombie.png"));
        public static ImageSource sourceLF1 = new BitmapImage(new Uri("pack://application:,,,/res/FastZombieLF1.png"));
        public static ImageSource sourceLF2 = new BitmapImage(new Uri("pack://application:,,,/res/FastZombieLF2.png"));
        public static ImageSource sourceRF1 = new BitmapImage(new Uri("pack://application:,,,/res/FastZombieRF1.png"));
        public static ImageSource sourceRF2 = new BitmapImage(new Uri("pack://application:,,,/res/FastZombieRF2.png"));
        //    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)
        public SlowZombie() : base(5, 10, 10, 2, source,64,64)
        {
            enemyImage = base.enemyImage;
        }
        int i = 0;
        int j = 0;
        override public void Follow(Rectangle playerLocation, Canvas gameScreen)
        {
            i++;
            if (i == 8)
            {
                j++;
                switch (j)
                {
                    case 0:
                        base.enemyImage.Fill = new ImageBrush(source);
                        break;
                    case 1:
                        base.enemyImage.Fill = new ImageBrush(sourceLF1);
                        break;
                    case 2:
                        base.enemyImage.Fill = new ImageBrush(sourceLF2);
                        break;
                    case 3:
                        base.enemyImage.Fill = new ImageBrush(sourceLF1);
                        break;
                    case 4:
                        base.enemyImage.Fill = new ImageBrush(source);
                        break;
                    case 5:
                        base.enemyImage.Fill = new ImageBrush(sourceRF1);
                        break;
                    case 6:
                        base.enemyImage.Fill = new ImageBrush(sourceRF2);
                        break;
                    case 7:
                        base.enemyImage.Fill = new ImageBrush(sourceRF1);
                        j = 0;
                        break;
                }
                i = 0;
            }
            base.Follow(playerLocation, gameScreen);
        }

    }
}