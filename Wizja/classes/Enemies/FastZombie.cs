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
    public class FastZombie : Enemy
    {
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/FastZombie.png"));
        //    public Enemy(int healthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)

        public FastZombie() : base(125, 10, 10, 3.5, source)
        { }
    }
}
