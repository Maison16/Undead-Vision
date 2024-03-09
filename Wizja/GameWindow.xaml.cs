using System;
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
using System.Timers;

namespace Wizja
{
    /// <summary>
    /// Logika interakcji dla klasy GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private System.Timers.Timer gameTimer= new System.Timers.Timer();
        public GameWindow()
        {
            InitializeComponent();
            //inicjalizacja gameTimera
            gameTimer.Interval = 16; //16 MILISEKUND
            gameTimer.Elapsed += GameTick;
            gameTimer.Start(); //od razu zaczyna timer gameTimer
            

            //ładowanie mapy
            ObjectLoader objectLoader = new ObjectLoader(gameCanvas);
            //dodawanie obiektu hudu
            HUD hud= new HUD(100, 30, 300, statCanvas);

            //Tworzenie i otwieranie shop do testów
            Shop itemshop = new Shop(shopCanvas, hud);
            itemshop.ShowShop();
        }

        private void GameTick(object sender, ElapsedEventArgs e)
        {
            
        }
    }
}
