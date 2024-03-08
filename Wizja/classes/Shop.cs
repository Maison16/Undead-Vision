using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wizja.classes
{
    public class Shop
    {
        public Canvas shopCanvas;

        // Konstruktor pobiera Canvas
        public Shop(Canvas shopCanvas)
        {
            this.shopCanvas = shopCanvas;
        }

        // Pokazywnaie Sklepu
        public void ShowShop()
        {
            // Zewnetrzne tlo
            shopCanvas.Background = Brushes.DarkRed;

            // Wewnętrzne tlo
            Rectangle insideBackground = new Rectangle
            {
                Width = 1640,
                Height = 750,
                Fill = Brushes.DarkSalmon
            };
            Canvas.SetLeft(insideBackground, 10);
            Canvas.SetTop(insideBackground, 60);
            shopCanvas.Children.Add(insideBackground);

            // Label Item Shop
            Label itemShopLabel = new Label();
            itemShopLabel.Content = "Item Shop";
            itemShopLabel.FontSize = 36;
            itemShopLabel.FontWeight = FontWeights.Bold;
            itemShopLabel.Foreground = Brushes.White;
            Canvas.SetLeft(itemShopLabel, insideBackground.Width / 2 - 80);
            shopCanvas.Children.Add(itemShopLabel);

            // Close button
            var closeButton = new Button();
            closeButton.Content = "Close";
            closeButton.FontSize = 24;
            closeButton.FontWeight = FontWeights.Bold;
            closeButton.Width = 100;
            closeButton.Height = 40;
            closeButton.Background = Brushes.Red;
            closeButton.Foreground = Brushes.White;
            closeButton.BorderBrush = Brushes.Black;
            closeButton.BorderThickness = new Thickness(3);
            Canvas.SetTop(closeButton, 10);
            Canvas.SetLeft(closeButton, 1540);
            closeButton.Click += HideShop;
            shopCanvas.Children.Add(closeButton);
        }

        //Ukrywanie Sklepu
        private void HideShop(object sender, EventArgs e)
        {
            shopCanvas.Children.Clear();
            shopCanvas.Background = null;
        }
    }
}
