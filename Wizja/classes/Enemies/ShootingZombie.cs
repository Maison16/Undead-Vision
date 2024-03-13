using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Wizja.Enemies;
using System.Windows.Controls;

namespace Wizja.classes
{
    public class ShootingZombie : Enemy
    {
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/Ghost.png"));
        //    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)
        public ShootingZombie(Canvas upperCanvas) : base(10, 10, 20, 4.5, source,86,104, upperCanvas)
        {
        }
    }
}
