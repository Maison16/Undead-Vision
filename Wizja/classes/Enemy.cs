using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Wizja.classes;

namespace Wizja.Enemies;
public class Enemy
{
    public int healthPoints;
    public int damagePoints;
    public int value; // Wartość pięniedzy które opuszcza po śmierci
    private double movingSpeed;
    public Rectangle enemyImage;
    public bool isLiving = false; // True jężeli przeciwnik żyje oraz jest na mapie
    private int coolDown = 62; // Co ileś ticków zadaje obrażenia
    private int tickCount = 0;
    static private Player player;


    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource, int Width, int Height)
    {
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
        enemyImage.RenderTransformOrigin = new Point(0.5, 0.5);
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
        healthPoints -= takenDamage;
        if (healthPoints > 0)
        {
            Console.WriteLine(!isLiving);
            return !isLiving; //True jeżeli żyje
        }
        else
        {
            if (isLiving)
            {
                player.hud.ChangeMoney(value);
                isLiving = false;//False jeżeli nie
            }
            return !isLiving;
        }
    }
    public void SetPlayer(Player player1)
    {
        player = player1;
    }
    // Porusz przeciwników w strone playera 
    public void Follow(Rectangle playerLocation, Canvas gameScreen)
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
        }
        else if (distance >= 40) // Jeżeli player jest daleko Losuj bardziej jego scieżke
        {
            dirX = (dx + MinusOrPlus() * rnd.Next(24, 48)) / distance;
            dirY = (dy + MinusOrPlus() * rnd.Next(24, 48)) / distance;
            x += dirX * movingSpeed;
            y += dirY * movingSpeed;
            Canvas.SetLeft(enemyImage, x);
            Canvas.SetTop(enemyImage, y);
        }
    }
    public void BreakCollision(Rectangle obj, Canvas gameScreen, Rectangle playerLocation)
    {
        Random rnd = new Random();
        double x = Canvas.GetLeft(enemyImage);
        double y = Canvas.GetTop(enemyImage);
        double dx;
        double dy;
        double or_x = x;
        double or_y = y;
        double dirX;
        double dirY;
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
        double distance = Math.Sqrt(dx * dx + dy * dy);
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
