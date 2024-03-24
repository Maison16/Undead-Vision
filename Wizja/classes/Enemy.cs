using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wizja.classes;
using T = System.Timers;

namespace Wizja.Enemies;
public class Enemy
{
    public int healthPoints;
    public int damagePoints;
    public int value; // Wartość pięniedzy które opuszcza po śmierci
    protected double movingSpeed;
    public Rectangle enemyImage;
    public Rectangle pointer;
    public bool isLiving = false; // True jężeli przeciwnik żyje oraz jest na mapie
    protected int coolDown = 62; // Co ileś ticków zadaje obrażenia
    protected int tickCount = 0;
    static protected Player player;
    protected Canvas upperCanvas;
    public DispatcherTimer timerBlackout;
    public DispatcherTimer timerPinging;
    //odtwarzanie asynchronicznie dostawania hitka
    public SoundPlayer auch = new SoundPlayer("sound/auch.wav");


    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource, int Width, int Height, Canvas upperCanvas)
    {
        this.upperCanvas = upperCanvas;
        this.healthPoints = helthPoints;
        this.damagePoints = damagePoints;
        this.value = value;
        this.movingSpeed = movingSpeed;
        enemyImage = new Rectangle()
        {
            Width = Width,
            Height = Height,
            Fill = new ImageBrush(imageSource),
            Name = "Enemy"
        };
        pointer = new Rectangle()
        {
            Width = 10,
            Height = 10,
            Fill = Brushes.DarkRed,
        };
        enemyImage.RenderTransformOrigin = new Point(0.5, 0.5);
        this.upperCanvas = upperCanvas;
        timerBlackout = new DispatcherTimer();
        timerPinging = new DispatcherTimer();
        timerBlackout.Interval = TimeSpan.FromMilliseconds(500);
        timerPinging.Interval = TimeSpan.FromMilliseconds(2500);
        timerPinging.Tick += Pinging;
        timerBlackout.Tick += Blackout;
    }

    private void Blackout(object sender, EventArgs e)
    {
        pointer.Visibility = Visibility.Hidden;
        timerBlackout.Stop();
        timerPinging.Start();
    }

    private void Pinging(object sender, EventArgs e)
    {
        pointer.Visibility = Visibility.Visible;
        timerPinging.Stop();
        timerBlackout.Start();
    }

    //Sprawdza kolizje między potworem a drugim obiektem
    public bool IsColision(Rect secondObject)
    {
        Rect hitbox = new Rect(Canvas.GetLeft(enemyImage), Canvas.GetTop(enemyImage), enemyImage.Width, enemyImage.Height);
        if (hitbox.IntersectsWith(secondObject))
            return true;
        else
            return false;
    }

    //public bool IsDead(int takenDamage,Map mapa)
    public bool IsDead(int takenDamage)
    {
        DispatcherTimer hitVisualizaionTimer = new DispatcherTimer();
        hitVisualizaionTimer.Interval = TimeSpan.FromMilliseconds(200);
        bool tick = false;

        // funkcja matiego do knockbacku dla przeciwnikow
        hitVisualizaionTimer.Tick += (sender, e) =>
        {
            if (!tick)
            {
                this.enemyImage.Opacity = 0.4;
                tick = true;
            }
            else
            {
                this.enemyImage.Opacity = 1;
                tick = false;
                hitVisualizaionTimer.Stop();
            }
        };
        hitVisualizaionTimer.Start();

        healthPoints -= takenDamage;
        if (healthPoints > 0)
        {
            
            return !isLiving; //True jeżeli żyje
        }
        else
        {
            if (isLiving)
            {
                player.hud.ChangeMoney(value);
                isLiving = false;//False jeżeli nie
                upperCanvas.Children.Remove(pointer);
                // zmniejszamy hitbox do 0x0, usuwanie przy smierci powodowalo bugi, a to dziala just fine
                enemyImage.Width = 0;
                enemyImage.Height = 0;
            }
            return !isLiving;
        }
    }

    public void SetPlayer(Player player1)
    {
        player = player1;
    }
    // Porusz przeciwników w strone playera 
    virtual public void Follow(Rectangle playerLocation, Canvas gameScreen)
    {
        Random rnd = new Random();
        double x = Canvas.GetLeft(enemyImage);
        double y = Canvas.GetTop(enemyImage);
        double dx = Canvas.GetLeft(playerLocation) - x;
        double dy = Canvas.GetTop(playerLocation) - y;
        double distance = Math.Sqrt(dx * dx + dy * dy);
        double dirX;
        double dirY;
        if (distance >= 40 && distance <= 120) // Jeżeli player jest dość blisko staraj podążać się obok niego 
        {
            dirX = (dx + MinusOrPlus() * rnd.Next(16)) / distance;
            dirY = (dy + MinusOrPlus() * rnd.Next(16)) / distance;
            x += dirX * movingSpeed;
            y += dirY * movingSpeed;
            Canvas.SetLeft(enemyImage, x);
            Canvas.SetTop(enemyImage, y);
            Canvas.SetLeft(pointer, x + (enemyImage.Width / 2));
            Canvas.SetTop(pointer, y + (enemyImage.Height / 2));
        }
        else if (distance >= 40) // Jeżeli player jest daleko Losuj bardziej jego scieżke
        {
            dirX = (dx + MinusOrPlus() * rnd.Next(24, 48)) / distance;
            dirY = (dy + MinusOrPlus() * rnd.Next(24, 48)) / distance;
            x += dirX * movingSpeed;
            y += dirY * movingSpeed;
            Canvas.SetLeft(enemyImage, x);
            Canvas.SetTop(enemyImage, y);
            Canvas.SetLeft(pointer, x + (enemyImage.Width / 2));
            Canvas.SetTop(pointer, y + (enemyImage.Height / 2)-10);
        }
    }

    public void HoldMove(Canvas gameScreen)
    {
        DispatcherTimer waitAfterHit = new DispatcherTimer();
        waitAfterHit.Interval = TimeSpan.FromMilliseconds(1000);
        movingSpeed -= 0.8;
        waitAfterHit.Tick += (sender, e) =>
        {
            movingSpeed += 0.8;
            waitAfterHit.Stop();
        };
        waitAfterHit.Start();
    }

    public void BreakCollision(Rectangle obj, Canvas gameScreen, Rectangle playerLocation)
    {
        double x = Canvas.GetLeft(enemyImage);
        double y = Canvas.GetTop(enemyImage);
        double dx;
        double dy;
        double distance;
        double or_x = x;
        double or_y = y;
        double dirX;
        double dirY;
        dx = Canvas.GetLeft(playerLocation) - x;
        dy = Canvas.GetTop(playerLocation) - y;
        distance = Math.Sqrt(dx * dx + dy * dy);
        if (distance > 1800) 
        {
            dirX = dx / distance;
            dirY = dy / distance;
            x += dirX * movingSpeed*100;
            y += dirY * movingSpeed*100;
            Canvas.SetLeft(enemyImage, x);
            Canvas.SetTop(enemyImage, y);
            return;
        }
    
        if (x > Canvas.GetLeft(playerLocation))
        {
            dx = 4000 - x;
        }
        else 
        {
            dx = 100 - x;
        }

        if (y > Canvas.GetTop(playerLocation))
        {
            dy = 3000 - y;
        }
        else
        {
            dy = 100 - y;
        }
        distance = Math.Sqrt(dx * dx + dy * dy);
        dirX = dx / distance;
        dirY = dy / distance;
        x = or_x + dirX * movingSpeed*1.5;
        y = or_y + dirY * movingSpeed*1.5;
        Canvas.SetLeft(enemyImage, x);
        Canvas.SetTop(enemyImage, y);
    }

    // Generuje 1 albo -1

    public int MinusOrPlus()
    {
        Random rando = new Random();
        int i = rando.Next(0, 2);
        if (i == 0)
        {
            i = -1;
        }
        return i;
    }

    public int DealDamage() 
    {
        if (tickCount == 0)
        {
            tickCount++;
            auch.Play();
            DispatcherTimer hitVisualizaionTimer = new DispatcherTimer();
            hitVisualizaionTimer.Interval = TimeSpan.FromMilliseconds(100);
            bool tick = false;
            int hitCounter = 0;
            hitVisualizaionTimer.Tick += (sender, e) =>
            {
                if (!tick)
                {
                    player.playerImage.Opacity = 0.4;
                    tick = true;
                }
                else
                {
                    player.playerImage.Opacity = 1;
                    tick = false;
                    hitCounter++;
                }
                if(hitCounter==4)
                {
                    hitVisualizaionTimer.Stop();
                }
            };
            hitVisualizaionTimer.Start();
            return damagePoints;
        }
        else if (tickCount < coolDown)
        {
            tickCount++;
        }
        else if (tickCount >= coolDown)
        {
            tickCount = 0;
        }
        return 0;
    }

}
