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
using System.DirectoryServices.ActiveDirectory;

namespace Wizja.classes.guns
{
    public class Shotgun : Weapon
    {
        public static BitmapImage img = new BitmapImage(new Uri("pack://application:,,,/res/shotgun.png"));
        public Shotgun() : base("Shotgun", 2, 160, 200, false, img) { this.SetCoolDown(35); }

        public override void Shoot(Point playerPos, Vector direction, List<Rectangle> targets, List<Enemy> enemies, Canvas gameCanvas)
        {
            Random random = new Random();
            Color color = Colors.Lime;

            double newRange1 = random.Next(-50, 1);
            Projectile projectile = new Projectile(playerPos.X, playerPos.Y, direction, 6, this.range + newRange1, this.dmg, this.pierce, color, targets, enemies, gameCanvas);

            double newRange2 = random.Next(-80, 1); // losowy zasieg
            float randomRotation2 = (float)(random.NextDouble() * (0.3 - 0.1) + 0.1); // losowy offset strzalu
            direction = RotateVector(direction, randomRotation2);
            Projectile projectile2 = new Projectile(playerPos.X, playerPos.Y, direction, 5, this.range + newRange2, this.dmg, this.pierce, color, targets, enemies, gameCanvas);

            double newRange3 = random.Next(-80, 1); // losowy zasieg
            float randomRotation3 = (float)(random.NextDouble() * (6 - 5.75) + 5.75); // losowy offset strzalu
            direction = RotateVector(direction, randomRotation3);
            Projectile projectile3 = new Projectile(playerPos.X, playerPos.Y, direction, 4, this.range + newRange3, this.dmg, this.pierce, color, targets, enemies, gameCanvas);
        }
    }
}
