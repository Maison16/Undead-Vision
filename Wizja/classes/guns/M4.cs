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
    public class M4 : Weapon
    {
        public static BitmapImage img = new BitmapImage(new Uri("pack://application:,,,/res/m4.png"));
        public M4() : base("M4A1", 5, 550, 400, img) { this.SetCoolDown(20); }

        public override void Shoot(Point playerPos, Vector direction, List<Rectangle> targets, List<Enemy> enemies, Canvas gameCanvas)
        {
            Projectile projectile = new Projectile(playerPos.X, playerPos.Y, direction, 6, this.range, this.dmg, targets, enemies, gameCanvas);
        }
    }
}
