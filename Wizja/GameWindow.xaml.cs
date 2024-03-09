﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wizja.classes;

namespace Wizja
{
    /// <summary>
    /// Logika interakcji dla klasy GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            HUD hud= new HUD(100, 30, 300, statCanvas);
            ObjectLoader objectLoader=new ObjectLoader(gameCanvas);

            //Tworzenie i otwieranie shop do testów
            Shop itemshop = new Shop(shopCanvas, hud);
            itemshop.ShowShop();
        }
       
    }
}
