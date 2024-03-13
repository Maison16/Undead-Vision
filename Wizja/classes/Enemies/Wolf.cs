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
    public class Wolf : Enemy
    {
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/Wolf.png"));
        public static ImageSource source1 = new BitmapImage(new Uri("pack://application:,,,/res/WolfLF1.png"));
        public static ImageSource source2 = new BitmapImage(new Uri("pack://application:,,,/res/WolfRF1.png"));
        //    public Enemy(int healthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)

        public Wolf() : base(7, 10, 10, 4, source, 74, 119)
        { }
        int i = 0, j = 0;
        override public void Follow(Rectangle playerLocation, Canvas gameScreen)
        {
            i++;
            if (i == 16)
            {
                j++;
                switch (j)
                {
                    case 0:
                        base.enemyImage.Fill = new ImageBrush(source1);
                        break;
                    case 1:
                        base.enemyImage.Fill = new ImageBrush(source2);
                        //j = 0;
                        break;
                    case 2:
                        base.enemyImage.Fill = new ImageBrush(source1);
                        break;
                    case 3:
                        base.enemyImage.Fill = new ImageBrush(source2);
                        j = 0;
                        break;
                }
                i = 0;
            }
            base.Follow(playerLocation, gameScreen);
        }
    }
}
