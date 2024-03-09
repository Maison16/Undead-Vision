using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Wizja.classes
{
    public class Shop
    {
        private Canvas shopCanvas;
        private Canvas statCanvas;
        //private int money;

        // Konstruktor pobiera Canvas
        public Shop(Canvas shopCanvas, Canvas statCanvas)
        {
            this.shopCanvas = shopCanvas;
            this.statCanvas = statCanvas;
        }

        // Pokazywnaie Sklepu
        public void ShowShop()
        {
            // -->  Sekcja ogólna

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

            // --> Sekcja Weapons

            // Label Item Shop
            Label weaponsLabel = new Label();
            weaponsLabel.Content = "Weapons";
            weaponsLabel.FontSize = 24;
            weaponsLabel.FontWeight = FontWeights.Bold;
            weaponsLabel.Foreground = Brushes.White;
            weaponsLabel.Background = Brushes.DarkRed;
            weaponsLabel.BorderBrush = Brushes.DarkRed;
            weaponsLabel.BorderThickness = new Thickness(5);
            Canvas.SetLeft(weaponsLabel, 300);
            Canvas.SetTop(weaponsLabel, 70);
            shopCanvas.Children.Add(weaponsLabel);

            // Konstruktor ele Weapons
            weaponsShop();

            // Sekcja Armor and HP
            Label armorLabel = new Label();
            armorLabel.Content = "Armor & HP";
            armorLabel.FontSize = 24;
            armorLabel.FontWeight = FontWeights.Bold;
            armorLabel.Foreground = Brushes.White;
            armorLabel.Background = Brushes.DarkRed;
            armorLabel.BorderBrush = Brushes.DarkRed;
            armorLabel.BorderThickness = new Thickness(5);
            Canvas.SetLeft(armorLabel, 1170);
            Canvas.SetTop(armorLabel, 70);
            shopCanvas.Children.Add(armorLabel);

            // Konstruktor Armor
            armorShop();
        }

        //////////////////////////////////// DO USUNIĘCIA TYLKO TEST \/ \/ \/
        public class Weapon
        {
            public string name { get; set; }
            public double dmg { get; set; }
            public double range { get; set; }
            public int cost { get; set; }
            public BitmapImage img { get; set; }

            public Weapon(string name, double dmg, double range, int cost, BitmapImage img)
            {
                this.name = name;
                this.dmg = dmg;
                this.range = range;
                this.cost = cost;
                this.img = img;
            }
        }
        // przykładowe bronie - do wyrzucenia potem !!!
        List<Weapon> weaponList = new List<Weapon>{
            new Weapon("gun", 20, 10, 100, new BitmapImage()),
            new Weapon("better gun", 30, 20, 200, new BitmapImage()),
            new Weapon("good gun", 40, 30, 300,new BitmapImage()),
            new Weapon("excellent gun", 50, 40, 400,new BitmapImage()),
            new Weapon("family guy", 60, 50, 500,new BitmapImage())
        };

        //////////////////////////////////// DO USUNIĘCIA TYLKO TEST /\ /\ /\

        //////////////////////////////////// DO USUNIĘCIA TYLKO TEST \/ \/ \/ 
        int money = 350;
        int j = 0;
        //////////////////////////////////// DO USUNIĘCIA TYLKO TEST /\ /\ /\

        private void weaponsShop()
        {
            for(int i=0;i<5; i++)
            {
                var w = weaponList[i];

                Rectangle rec = new Rectangle();
                rec.Height = 64;
                rec.Width = 64;
                /////// jak będą już zdjęcia \/\/\/ \/\/\/
                //ImageBrush imgB = new ImageBrush(w.img);
                //rec.Fill = imgB;
                /////// /\ /\ /\ /\ /\ /\ /\ /\ /\ /\ /\ /\
                //////////////////////////////////// DO USUNIĘCIA TYLKO TEST \/ \/ \/ 
                rec.Fill = Brushes.White;
                //////////////////////////////////// DO USUNIĘCIA TYLKO TEST /\ /\ /\
                Canvas.SetLeft(rec, 120);
                Canvas.SetTop(rec, 150 + i * 110);
                shopCanvas.Children.Add(rec);

                Label lblUp = new Label();
                lblUp.Content = w.name;
                lblUp.FontSize = 36;
                lblUp.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(lblUp, 200);
                Canvas.SetTop(lblUp, 150 + i * 110);
                shopCanvas.Children.Add(lblUp);

                Label lblDown = new Label();
                lblDown.Content = "dmg:"+w.dmg+" range:"+w.range;
                lblDown.FontSize = 24;
                lblDown.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(lblDown, 210);
                Canvas.SetTop(lblDown, 190 + i * 110);
                shopCanvas.Children.Add(lblDown);

                Button buyButton = new Button();
                buyButton.Content = w.cost;
                buyButton.FontSize = 24;
                buyButton.FontWeight = FontWeights.Bold;
                buyButton.Width = 100;
                buyButton.Height = 40;
                buyButton.Background = Brushes.Gray;
                buyButton.Foreground = Brushes.White;
                buyButton.BorderBrush = Brushes.Black;
                buyButton.BorderThickness = new Thickness(3);
                Canvas.SetLeft(buyButton, 500);
                Canvas.SetTop(buyButton, 165 + i * 110);
                if (w.cost < money)
                {
                    buyButton.Background = Brushes.Green;
                    buyButton.Click += buyClick;
                }
                void buyClick(object sender, RoutedEventArgs e)
                {
                    Buy(w);
                }
                shopCanvas.Children.Add(buyButton);
            }
        }


        private void Buy(Weapon w) 
        {
            //////////////////////////////////// DO USUNIĘCIA TYLKO TEST \/ \/ \/ 
            Label buyInfo = new Label();
            buyInfo.Content = " Zapłacono:" +w.cost + "money:" + money;
            buyInfo.FontSize = 26;
            buyInfo.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(buyInfo, 610);
            Canvas.SetTop(buyInfo, 190 + j * 110);
            money -= w.cost;
            j++;

            shopCanvas.Children.Clear();
            ShowShop();

            shopCanvas.Children.Add(buyInfo);
            //////////////////////////////////// DO USUNIĘCIA TYLKO TEST /\ /\ /\

            /*            
                          if (statCanvas.GetMoney() > w.cost)
                        {
                            w.cost *= -1;
                            statCanvas.ChangeMoney(w.cost);
                        }
            */
        }

        private void armorShop()
        {
            for (int i = 0; i < 5; i++)
            {
                var w = weaponList[i];

                Rectangle rec = new Rectangle();
                rec.Height = 64;
                rec.Width = 64;
                /////// jak będą już zdjęcia \/\/\/ \/\/\/
                //ImageBrush imgB = new ImageBrush(w.img);
                //rec.Fill = imgB;
                /////// /\ /\ /\ /\ /\ /\ /\ /\ /\ /\ /\ /\
                //////////////////////////////////// DO USUNIĘCIA TYLKO TEST \/ \/ \/ 
                rec.Fill = Brushes.White;
                //////////////////////////////////// DO USUNIĘCIA TYLKO TEST /\ /\ /\
                Canvas.SetLeft(rec, 1020);
                Canvas.SetTop(rec, 150 + i * 110);
                shopCanvas.Children.Add(rec);

                Label lblUp = new Label();
                lblUp.Content = w.name;
                lblUp.FontSize = 36;
                lblUp.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(lblUp, 1100);
                Canvas.SetTop(lblUp, 150 + i * 110);
                shopCanvas.Children.Add(lblUp);

                Label lblDown = new Label();
                lblDown.Content = "dmg:" + w.dmg + " range:" + w.range;
                lblDown.FontSize = 24;
                lblDown.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(lblDown, 1110);
                Canvas.SetTop(lblDown, 190 + i * 110);
                shopCanvas.Children.Add(lblDown);

                Button buyButton = new Button();
                buyButton.Content = w.cost;
                buyButton.FontSize = 24;
                buyButton.FontWeight = FontWeights.Bold;
                buyButton.Width = 100;
                buyButton.Height = 40;
                buyButton.Background = Brushes.Gray;
                buyButton.Foreground = Brushes.White;
                buyButton.BorderBrush = Brushes.Black;
                buyButton.BorderThickness = new Thickness(3);
                Canvas.SetLeft(buyButton, 1400);
                Canvas.SetTop(buyButton, 165 + i * 110);
                if (w.cost < money)
                {
                    buyButton.Background = Brushes.Green;
                    buyButton.Click += buyClick;
                }
                void buyClick(object sender, RoutedEventArgs e)
                {
                    Buy(w);
                }
                shopCanvas.Children.Add(buyButton);
            }
        }

        //Ukrywanie Sklepu
        private void HideShop(object sender, EventArgs e)
        {
            shopCanvas.Children.Clear();
            shopCanvas.Background = null;
        }
    }
}
