using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            hpLabel = new Label
            {
                Content = $"Hp: {hp}",
                FontSize = 50,
                Foreground = Brushes.Gold
            };
            Canvas.SetLeft(hpLabel, 50);
            Canvas.SetTop(hpLabel, 15);
            statCanvas.Children.Add(hpLabel);

            timeLabel = new Label
            {
                Content = $"Time: {time}s",
                FontSize = 50,
                Foreground = Brushes.Gold
            };
            Canvas.SetLeft(timeLabel, 850);
            Canvas.SetTop(timeLabel, 15);
            statCanvas.Children.Add(timeLabel);

            moneyLabel = new Label
            {
                Content = $"Money: {money}",
                FontSize = 50,
                Foreground = Brushes.Gold
            };
            Canvas.SetLeft(moneyLabel, 1500);
            Canvas.SetTop(moneyLabel, 15);
            statCanvas.Children.Add(moneyLabel);

            nearBox = new Rectangle
            {
                Width = 200,
                Height = 100,
                Fill = Brushes.DarkRed
            };
            Canvas.SetLeft(nearBox, 1720);
            Canvas.SetTop(nearBox, 980);
            statCanvas.Children.Add(nearBox);
            nearLabel = new Label
            {
                Content = $"  You are\n" +
                $"near shop!",
                FontSize = 32,
                Foreground = Brushes.Gold
            };
            Canvas.SetLeft(nearLabel, 1750);
            Canvas.SetTop(nearLabel, 980);
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
            timeLabel.Content = $"Time: {time}s";
        }
        private void HpLabelSet()
        {
            hpLabel.Content = $"Hp: {hp}";
        }
        private void MoneyLabelSet()
        {
            moneyLabel.Content = $"Money: {money}";
        }
    }
}
