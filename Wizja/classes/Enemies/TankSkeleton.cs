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
    public class TankSkeleton : Enemy
    {
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/TankSkeleton.png"));
        //    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)
        public TankSkeleton(Canvas upperCanvas) : base(50, 10, 10, 2.5, source,106,110, upperCanvas)
        { }
    }
}
