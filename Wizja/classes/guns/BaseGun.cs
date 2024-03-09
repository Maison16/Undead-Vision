using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Timers;
using System.Windows;

namespace Wizja.classes.guns
{
    public class BaseGun : Weapon
    {
        public static BitmapImage img = new BitmapImage(new Uri("pack://application:,,,/res/FastZombie.png"));
        public BaseGun() : base("Gun1", 1, 250, 0, img) { }

        public override void Shoot(Point playerPos, Point endPoint, ObjectLoader objectLoader, Canvas gameCanvas)
        {
            Projectile projectile = new Projectile(playerPos.X, playerPos.Y, endPoint.X, endPoint.Y, 2, objectLoader.GetListMapObjects(), gameCanvas);
        }
    }
}
