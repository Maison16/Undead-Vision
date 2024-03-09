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
using System.Windows.Shapes;
using Wizja.Enemies;

namespace Wizja.classes.guns
{
    public class Shotgun : Weapon
    {
        public static BitmapImage img = new BitmapImage(new Uri("pack://application:,,,/res/shotgun.png"));
        public Shotgun() : base("Shotgun", 2, 200, 200, img) { }

        public override void Shoot(Point playerPos, Vector direction, List<Rectangle> targets, List<Enemy> enemies, Canvas gameCanvas)
        {
            Projectile projectile = new Projectile(playerPos.X, playerPos.Y, direction, 20, 200, 2, targets, enemies, gameCanvas);
        }
    }
}
