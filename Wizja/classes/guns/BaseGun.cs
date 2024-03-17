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
    public class BaseGun : Weapon
    {
        public static BitmapImage img = new BitmapImage(new Uri("pack://application:,,,/res/pistol.png"));
        public BaseGun() : base("Pistol", 2, 350, 100, false, img) // obrazenia, zasieg, koszt
        {
            this.SetCoolDown(15);
        } 

        public override void Shoot(Point playerPos, Vector direction, List<Rectangle> targets, List<Enemy> enemies, Canvas gameCanvas)
        {
            Projectile projectile = new Projectile(playerPos.X, playerPos.Y, direction, 5, this.range, this.dmg, this.pierce, Colors.Gold, targets, enemies, gameCanvas);
        }
    }
}
