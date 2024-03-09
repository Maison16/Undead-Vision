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
using System.Windows.Threading;
using Wizja.Enemies;

namespace Wizja.classes
{
    public class Shop
    {
        private Player player;
        private Canvas shopCanvas;
        private Canvas gameCanvas;
        private HUD hud;
        private ObjectLoader objectLoader;
        private ImageBrush shopImage;
        private Rectangle shopBuild;
        private Rect shopRange;
        private Rect playerHitBox;
        private List<Rectangle> listMapObjects;
        private List<Rectangle> listHitObjects;
        public DispatcherTimer timerShopCheck;
        GameWindow gameWindow;
        // Konstruktor pobiera Canvas i hud (money)
        public Shop(Canvas gameCanvas, Canvas shopCanvas, HUD hud, GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;
            this.player = player;
            this.gameCanvas = gameCanvas;
            this.shopCanvas = shopCanvas;
            this.hud = hud;
            timerShopCheck = new DispatcherTimer();
            timerShopCheck.Interval = TimeSpan.FromMilliseconds(16);
            timerShopCheck.Tick += NearChecker;
            objectLoader = new ObjectLoader(gameCanvas);
            shopImage = new ImageBrush();
            listMapObjects = objectLoader.GetListMapObjects();
            listHitObjects = objectLoader.GetListMovingObjects();
            shopBuild = objectLoader.BuildShop(3000, 1700, shopImage, 400, 300);
            listMapObjects.Add(shopBuild);
            listHitObjects.Add(shopBuild);
            ShopIsOpen();
            gameCanvas.Children.Add(shopBuild);
        }
        public void initPlayer(Player player)
        {
            this.player = player;
        }
        bool isShow = false;
        bool onePress = true;
        private void NearChecker(object sender, EventArgs e)
        {
            shopRange = new Rect(Canvas.GetLeft(shopBuild)-20, Canvas.GetTop(shopBuild)-20, shopBuild.Width+40, shopBuild.Height+40);
            playerHitBox = new Rect(Canvas.GetLeft(player.playerImage), Canvas.GetTop(player.playerImage), player.playerImage.Width, player.playerImage.Height);
            if (playerHitBox.IntersectsWith(shopRange))
            {
                hud.NearShopShow();
                ShopIsOpen();

                if (gameWindow.getB() == true)
                {
                    if (isShow == false && onePress == true)
                    {
                        ShowShop();
                        isShow = true;
                    }
                    else if (isShow == true && onePress == true)
                    {
                        shopCanvas.Children.Clear();
                        shopCanvas.Background = null;
                        isShow = false;
                    }
                    onePress = false;
                }
                else
                {
                    onePress = true;
                }
            }
            else
            {
                hud.NearShopHide();
                ShopIsClose();
                shopCanvas.Children.Clear();
                shopCanvas.Background = null;
            }
        }

        public void ShopIsClose()
        {
            shopImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/shopClosed.png"));
            shopBuild.Fill= shopImage;
        }
        public void ShopIsOpen()
        {
            shopImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/shopOpen.png"));
            shopBuild.Fill = shopImage;
        }
        // Pokazywnaie Sklepu
        public void ShowShop()
        {
            /// Sekcja ogólna

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

            /// Sekcja Weapons

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

        // przykładowe bronie - do wyrzucenia potem !!!
        List<Weapon> weaponList = new List<Weapon>{
            new guns.BaseGun(),
            new guns.Shotgun(),
            new guns.BaseGunAuto(),
            new guns.M4(),
            new guns.FamilyGun()
        };


        int[] used = {0,0,0,0,0};

        // Sklep z bronia
        private void weaponsShop()
        {
            for(int i=0;i<5; i++)
            {
                var w = weaponList[i];

                Rectangle rec = new Rectangle();
                rec.Height = 128;
                rec.Width = 128;
                ImageBrush imgB = new ImageBrush(w.img) ;
                rec.Fill = imgB;
                Canvas.SetLeft(rec, 60);
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
                if (used[weaponList.IndexOf(w)] == 1)
                {
                    buyButton.Background = Brushes.Maroon;
                }
                else if (w.cost <= hud.GetMoney())
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

        // Odejmowanie pieniędzy z hudu tylko kupywanie broni
        private void Buy(Weapon w) 
        {
            hud.ChangeMoney(w.cost * -1);
            shopCanvas.Children.Clear();
            used[weaponList.IndexOf(w)] = 1;
            player.setWeapon(w);
            ShowShop();
        }

        int[] usedArmor = {1,0,0};
        // Sklep z armorem
        private void armorShop()
        {   
            //hp potions
            for(int i=1; i <= 3; i++)
            {
                int amount = (int)Math.Pow(2, i - 1);
                Rectangle rec = new Rectangle();
                rec.Height = 128;
                rec.Width = 128;
                ImageBrush imgB = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/res/HPpotion.png")));
                rec.Fill = imgB;
                Canvas.SetLeft(rec, 960);
                Canvas.SetTop(rec, 40 + i * 110);
                shopCanvas.Children.Add(rec);

                Label lblUp = new Label();
                lblUp.Content = 25 * amount + " HP potion";
                lblUp.FontSize = 36;
                lblUp.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(lblUp, 1100);
                Canvas.SetTop(lblUp, 40 + i * 110);
                shopCanvas.Children.Add(lblUp);

                Label lblDown = new Label();
                lblDown.Content = "heals "+ 25 * amount + " player's HP";
                lblDown.FontSize = 24;
                lblDown.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(lblDown, 1110);
                Canvas.SetTop(lblDown, 80 + i * 110);
                shopCanvas.Children.Add(lblDown);

                Button buyButton = new Button();
                buyButton.Content = 75 * amount + "";
                buyButton.FontSize = 24;
                buyButton.FontWeight = FontWeights.Bold;
                buyButton.Width = 100;
                buyButton.Height = 40;
                buyButton.Background = Brushes.Gray;
                buyButton.Foreground = Brushes.White;
                buyButton.BorderBrush = Brushes.Black;
                buyButton.BorderThickness = new Thickness(3);
                Canvas.SetLeft(buyButton, 1400);
                Canvas.SetTop(buyButton, 55 + i * 110);
                if (hud.GetHp() >= 100)
                {
                    buyButton.Background = Brushes.Maroon;
                }
                else if (hud.GetMoney() >= i*75)
                {
                    buyButton.Background = Brushes.Green;
                    buyButton.Click += buyClick;
                }
                void buyClick(object sender, RoutedEventArgs e)
                {
                    if (hud.GetHp() + 25 * amount > 100)
                    {
                        hud.SetHp(100);
                    }
                    else
                    {
                        hud.SetHp(hud.GetHp() + 25 * amount);
                    }
                    hud.ChangeMoney(75 * amount * -1);
                    shopCanvas.Children.Clear();
                    ShowShop();
                }
                shopCanvas.Children.Add(buyButton);
            }


            if (hud.GetHp() <= 125)
                usedArmor[2] = 0;
            if (hud.GetHp() <= 100)
                usedArmor[1] = 0;

            //armors
            for (int j = 1; j <= 2; j++)
            {
                int amount = j;
                Rectangle rec = new Rectangle();
                rec.Height = 128;
                rec.Width = 128;
                ImageBrush imgB = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/res/armor.png")));
                rec.Fill = imgB;
                Canvas.SetLeft(rec, 960);
                Canvas.SetTop(rec, 370 + amount * 110);
                shopCanvas.Children.Add(rec);

                Label lblUp = new Label();
                lblUp.Content = 20 * amount + " Armor";
                lblUp.FontSize = 36;
                lblUp.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(lblUp, 1100);
                Canvas.SetTop(lblUp, 370 + amount * 110);
                shopCanvas.Children.Add(lblUp);

                Label lblDown = new Label();
                lblDown.Content = "adds " + 20 * amount + " player's HP";
                lblDown.FontSize = 24;
                lblDown.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(lblDown, 1110);
                Canvas.SetTop(lblDown, 410 + amount * 110);
                shopCanvas.Children.Add(lblDown);

                Button buyButton = new Button();
                buyButton.Content = 100 * amount + "";
                buyButton.FontSize = 24;
                buyButton.FontWeight = FontWeights.Bold;
                buyButton.Width = 100;
                buyButton.Height = 40;
                buyButton.Background = Brushes.Gray;
                buyButton.Foreground = Brushes.White;
                buyButton.BorderBrush = Brushes.Black;
                buyButton.BorderThickness = new Thickness(3);
                Canvas.SetLeft(buyButton, 1400);
                Canvas.SetTop(buyButton, 385 + amount * 110);
                if (usedArmor[amount] == 1)
                {
                    buyButton.Background = Brushes.Maroon;
                }
                else if (hud.GetMoney() >= amount * 100)
                {
                    buyButton.Background = Brushes.Green;
                    buyButton.Click += buyClick;
                }
                void buyClick(object sender, RoutedEventArgs e)
                {
                    hud.SetHp(hud.GetHp() + 20 * amount);
                    hud.ChangeMoney(-1 * amount * 100);
                    usedArmor[amount] = 1;
                    shopCanvas.Children.Clear();
                    ShowShop();
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
