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

namespace Wizja
{
    /// <summary>
    /// Logika interakcji dla klasy GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        int direction = 0x0000;
        public GameWindow()
        {
            InitializeComponent();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            int direction = 0x0000;

            if (e.Key == Key.W)
                direction += 0x1000;

            if (e.Key == Key.A)
                direction += 0x0100;

            if (e.Key == Key.S)
                direction += 0x0010;

            if (e.Key == Key.D)
                direction += 0x0001;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
                direction -= 0x1000;

            if (e.Key == Key.A)
                direction -= 0x0100;

            if (e.Key == Key.S)
                direction -= 0x0010;

            if (e.Key == Key.D)
                direction -= 0x0001;
        }
    }
}
