using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Wizja.classes;

namespace Wizja.Enemies;
public class Enemy
{
    public int helthPoints;
    public int damagePoints;
    public int value;
    public double movingSpeed;
    public Rectangle enemyImage;
    public bool isLiving = false; // True jężeli przeciwnik żyje oraz jest na mapie
    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)
    {
        this.helthPoints=helthPoints;
        this.damagePoints=damagePoints;
        this.value=value;
        this.movingSpeed=movingSpeed;
        enemyImage = new Rectangle()
        {
            Width = 64,
            Height = 64,
            Fill = new ImageBrush(imageSource)
        };
        enemyImage.RenderTransformOrigin =  new Point(0.5, 0.5);
    }
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
        helthPoints -= takenDamage;
        if (helthPoints > 0)
        {
            return !isLiving; //True jeżeli żyje
        }
        else
        {
            //Map.AddCoins(money);
            isLiving = false;//False jeżeli nie
            return !isLiving;
        }
    }
    public void Follow(Rectangle playerLocation) 
    {
        double x = Canvas.GetLeft(enemyImage);
        double y = Canvas.GetTop(enemyImage);
        double dx = Canvas.GetLeft(playerLocation) - x;
        double dy = Canvas.GetTop(playerLocation) - y;
        double distance = Math.Sqrt(dx * dx + dy * dy);
        if (distance >= 40)
        {
            // Calculate normalized direction vector
            double dirX = dx / distance;
            double dirY = dy / distance;

            // Move the projectile towards the target
            x += dirX * movingSpeed;
            y += dirY * movingSpeed;
            Canvas.SetLeft(enemyImage, x);
            Canvas.SetTop(enemyImage, y);
        }
    }
}
