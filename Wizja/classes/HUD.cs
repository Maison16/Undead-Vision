using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wizja.classes
{
    public class HUD
    {   
        private Canvas statCanvas;
        private int hp;
        private int time;
        private int money;
        private int totalCoins;
        private Label costLabel;
        private Label hpLabel;
        private Label timeLabel;
        

        public HUD(int hp, int time, int money, Canvas statCanvas)
        {
            this.hp = hp;
            this.time = time;
            this.money = money;
            this.statCanvas = statCanvas;

            Rectangle statBar = new Rectangle
            {
                Width = 1920,
                Height = 200,
                Fill = Brushes.Gray
            };
            Canvas.SetLeft(statBar, 0);
            Canvas.SetTop(statBar, 0);
            statCanvas.Children.Add(statBar);

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
        }
        public void SetMoney(int Money)
        {
            money = Money;
            MoneyLabelSet();
        }
        private void TimeLabelSet()
        {
             
        }
        private void HpLabelSet()
        {

        }
        private void MoneyLabelSet()
        {

        }
    }
}
