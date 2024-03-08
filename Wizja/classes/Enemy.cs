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
    public Rectangle imageType;
    public Enemy(int helthPoints, int damagePoints, int value, double movingSpeed, ImageSource imageSource)
    {
        this.helthPoints=helthPoints;
        this.damagePoints=damagePoints;
        this.value=value;
        this.movingSpeed=movingSpeed;
        imageType = new Rectangle()
        {
            Width = 32,
            Height = 32,
            Fill = new ImageBrush(imageSource)
        };
        imageType.RenderTransformOrigin =  new Point(0.5,0.5);
    }
    public bool IsColision(Rect secondObject)
    {
        Rect hitbox = new Rect(Canvas.GetLeft(imageType), Canvas.GetTop(imageType), imageType.Width, imageType.Height);
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
            return false;
        else
        {
            //Map.AddCoins(money);
            return true;
        }
    }
    public void Follow() 
    {

    }
}
