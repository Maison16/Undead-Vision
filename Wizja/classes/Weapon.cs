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
        protected bool pierce; // czy przebija przeciwnika
        public BitmapImage img { get; set; }
        public int coolDown { get; set; }
        public Weapon(string name, int dmg, double range, int cost, bool pierce, BitmapImage img)
        {
            this.name = name;
            this.dmg = dmg;
            this.range = range;
            this.range = range;
            this.cost = cost;
            this.img = img;
            this.pierce = pierce;
        }
        public Vector RotateVector(Vector vector, float angle)
        {
            double cosTheta = (float)Math.Cos(angle);
            double sinTheta = (float)Math.Sin(angle);

            double newX = vector.X * cosTheta - vector.Y * sinTheta;
            double newY = vector.X * sinTheta + vector.Y * cosTheta;

            return new Vector(newX, newY);
        }

        public void SetCoolDown(int cd) 
        {
            this.coolDown = cd;
        }
        public abstract void Shoot(Point playerPos, Vector direction, List<Rectangle> mapObjects, List<Enemy> enemies, Canvas gameCanvas);
    }
}
