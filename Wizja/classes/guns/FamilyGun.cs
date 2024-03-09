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
    public class FamilyGun : Weapon
    {
        public static BitmapImage img = new BitmapImage(new Uri("pack://application:,,,/res/familyguy.png"));
        public FamilyGun() : base("Family Gun", 30, 900, 200, img) { }

        public override void Shoot(Point playerPos, Vector direction, List<Rectangle> targets, List<Enemy> enemies, Canvas gameCanvas)
        {
            Projectile projectile = new Projectile(playerPos.X, playerPos.Y, direction, 30, 900, 10, targets, enemies, gameCanvas);
        }
    }
}
