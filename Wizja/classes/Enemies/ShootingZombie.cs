﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Wizja.Enemies;

namespace Wizja.classes
{
    public class ShootingZombie : Enemy
    {
        public static ImageSource source = new BitmapImage(new Uri("pack://application:,,,/res/Ghost.png"));
        //    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)
        public ShootingZombie() : base(15, 10, 20, 3.5, source,86,104)
        { }
    }
}
