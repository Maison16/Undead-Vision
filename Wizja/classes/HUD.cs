using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Wizja.classes
{
    public class HUD
    {   private DispatcherTimer timer = new DispatcherTimer();  
        private Canvas statCanvas;
        private int hp;
        private int time;
        private int money;
        private Label moneyLabel;
        private Label hpLabel;
        private Label timeLabel;
        private Label nearLabel;
        private Rectangle nearBox;
        private ImageBrush hpImage = new ImageBrush();
        private ImageBrush coinImage = new ImageBrush();
        private ImageBrush clockImage = new ImageBrush();
        public int totalPoints;

        public HUD(int hp, int time, int money, Canvas statCanvas)
        {
            this.hp = hp;
            this.time = time;
            this.money = money;
            this.statCanvas = statCanvas;

            Rectangle statBar = new Rectangle
            {
                Width = statCanvas.Width,
                Height = statCanvas.Height,
                Fill = Brushes.DarkRed
            };
            Canvas.SetLeft(statBar, 0);
            Canvas.SetTop(statBar, 0);
            statCanvas.Children.Add(statBar);
            hpImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/hpIcon.png"));
            Rectangle hpIcon = new Rectangle
            {
                Width = 50,
                Height = 50,
                Fill = hpImage
            };
            Canvas.SetLeft(hpIcon, 50);
            Canvas.SetTop(hpIcon, 30);
            statCanvas.Children.Add(hpIcon);
            hpLabel = new Label
            {
                Content = $"{hp}",
                FontSize = 50,
                Foreground = Brushes.Gold
            };
            Canvas.SetLeft(hpLabel, 100);
            Canvas.SetTop(hpLabel, 15);
            statCanvas.Children.Add(hpLabel);

            clockImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/clockIcon.png"));
            Rectangle clockIcon = new Rectangle
            {
                Width = 50,
                Height = 50,
                Fill = clockImage
            };
            Canvas.SetLeft(clockIcon, 900);
            Canvas.SetTop(clockIcon, 30);
            statCanvas.Children.Add(clockIcon);

            timeLabel = new Label
            {
                Content = $"{time}s",
                FontSize = 50,
                Foreground = Brushes.Gold
            };
            Canvas.SetLeft(timeLabel, 950);
            Canvas.SetTop(timeLabel, 15);
            statCanvas.Children.Add(timeLabel);
            coinImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/coinIcon.png"));
            Rectangle coinIcon = new Rectangle
            {
                Width = 50,
                Height = 50,
                Fill = coinImage
            };
            Canvas.SetLeft(coinIcon, 1650);
            Canvas.SetTop(coinIcon, 30);
            statCanvas.Children.Add(coinIcon);

            moneyLabel = new Label
            {
                Content = $"{money}",
                FontSize = 50,
                Foreground = Brushes.Gold
            };
            Canvas.SetLeft(moneyLabel, 1700);
            Canvas.SetTop(moneyLabel, 15);
            statCanvas.Children.Add(moneyLabel);

            nearBox = new Rectangle
            {
                Width = 400,
                Height = 40,
                Fill = Brushes.DarkRed
            };
            Canvas.SetLeft(nearBox, 1595);
            Canvas.SetTop(nearBox, 1040);
            statCanvas.Children.Add(nearBox);
            nearLabel = new Label
            {
                Content = "Press 'B' to open Shop",
                FontSize = 32,
                Foreground = Brushes.Gold
            };
            Canvas.SetLeft(nearLabel, 1595);
            Canvas.SetTop(nearLabel, 1030);
            statCanvas.Children.Add(nearLabel);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000); 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time--;
            TimeLabelSet();
            if (time == 0)
            {
                timer.Stop();
            }

        }
        public void NearShopShow()
        {
            nearBox.Visibility = Visibility.Visible;
            nearLabel.Visibility = Visibility.Visible;

        }
        public void NearShopHide()
        {
            nearBox.Visibility = Visibility.Hidden;
            nearLabel.Visibility = Visibility.Hidden;
        }
        public int GetHp()
        {
            return hp;
        }
        public int GetTime()
        {
            return time;
        }
        public int GetMoney()
        {
            return money;
        }
        public void SetHp(int Hp)
        {
            hp = Hp;
            HpLabelSet();
        }
        public void SetTime(int Time)
        {
            time = Time;
            TimeLabelSet();
            timer.Start();
        }
        public void SetMoney(int Money)
        {
            money = Money;
            MoneyLabelSet();
        }
        public void ChangeMoney(int Money)
        {
            money += Money;
            if(Money >= 0) 
            {
                totalPoints += Money;
            }
            MoneyLabelSet();
        }
        public void ChangeHp(int Hp)
        {
            hp += Hp;
            if (hp <= 0)
            {
                hp = 0;
            }
            HpLabelSet();
        }
        private void TimeLabelSet()
        {
            timeLabel.Content = $"{time}s";
        }
        private void HpLabelSet()
        {
            hpLabel.Content = $"{hp}";
        }
        private void MoneyLabelSet()
        {
            moneyLabel.Content = $"{money}";
        }
    }
}
