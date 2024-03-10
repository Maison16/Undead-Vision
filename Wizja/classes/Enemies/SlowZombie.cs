using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Wizja.Enemies;

namespace Wizja.classes
{
    public class SlowZombie : Enemy
    {
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/SlowZombie.png"));
        //    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)
        public SlowZombie() : base(5, 10, 10, 2.5, source,64,64)
        { }
    }
}