﻿using System;
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
        private string name { get; set; } // nazwa
        private int dmg { get; set; } // ilosc obrazen
        private double range { get; set; } // max zasieg wystrzeliwanego pocisku
        private int cost { get; set; } // koszt w sklepie
        private BitmapImage img { get; set; }

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
