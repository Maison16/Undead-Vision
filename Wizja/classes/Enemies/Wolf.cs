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
    public class Wolf : Enemy
    {
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/Wolf.png"));
        //    public Enemy(int healthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)

        public Wolf() : base(7, 10, 10, 3.5, source, 74, 119)
        { }
    }
}
