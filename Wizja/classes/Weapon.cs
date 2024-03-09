using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wizja.Enemies;

namespace Wizja.classes
{

    public abstract class Weapon
    {
        public string name { get; set; } // nazwa
        public int dmg { get; set; } // ilosc obrazen
        public double range { get; set; } // max zasieg wystrzeliwanego pocisku
        public int cost { get; set; } // koszt w sklepie
        public BitmapImage img { get; set; }

        public Weapon(string name, int dmg, double range, int cost, BitmapImage img)
        {
            this.name = name;
            this.dmg = dmg;
            this.range = range;
            this.range = range;
            this.cost = cost;
            this.img = img;
        }

        public abstract void Shoot(Point playerPos, Vector direction, List<Rectangle> mapObjects, List<Enemy> enemies, Canvas gameCanvas);
    }
}
