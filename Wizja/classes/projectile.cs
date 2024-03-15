using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wizja.Enemies;
using static System.Net.Mime.MediaTypeNames;
using T = System.Timers;

namespace Wizja.classes
{
    public class Projectile
    {
        private Point start; // poczatek linii
        private Point end; // koniec linii

        private int damage; // obrazenia pocisku
        private double thickness; // grubos linii
        private Color color; // kolor linii

        private List<Rect> hitBoxes; // lista hitboxow generowanych w celu znalezienia konca linii etc
        private Line projectileLine; // linia rysowana na canvasie

        private Canvas gameCanvas; // canvas
        private T.Timer timer; // timer

        public Projectile(double startX, double startY, Vector direction, double thickness, double range, int damage, Color color, List<Rectangle> mapObjects, List<Enemy> enemies, Canvas gameCanvas)
        {
            this.start = new Point(startX, startY);
            this.end = new Point(start.X + direction.X * range, start.Y + direction.Y * range); // obliczenie konca linii

            this.damage = damage;
            this.thickness = thickness;
            this.color = color;
            this.gameCanvas = gameCanvas;

            this.hitBoxes = GenerateHitBoxes(); // wygeneruj hitboxy aby potem sprawdzic

            // dla kazdej przeszkody na mapie sprawdz czy nachodzi ona z linia strzalu
            foreach (Rectangle target in mapObjects)
            {
                if (Collision(target)){
                    break;
                }
            }
            // dla kazdego hitboxa linii strzalu sprawdzamy czy nachodzi z przeciwnikem
            foreach (Rect bulletHitbox in hitBoxes)
            {
                if (CollisionCheckEnemy(bulletHitbox, enemies))
                {
                    break;
                }
            }
            // narysuj linie po ustaleniu konca linii
            DrawLine();

            // timer do zanikania
            timer = new T.Timer();
            timer.Interval = 40;
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = false;
            timer.Start();
        }

        private void OnTimerElapsed(object sender, EventArgs e)
        {
            gameCanvas.Dispatcher.Invoke(() =>
            {
                gameCanvas.Children.Remove(projectileLine);
            });
        }

        public void DrawLine()
        {
            projectileLine = new Line();
            projectileLine.Stroke = new SolidColorBrush(color);
            projectileLine.StrokeThickness = thickness;
            projectileLine.X1 = start.X;
            projectileLine.Y1 = start.Y;
            projectileLine.X2 = end.X;
            projectileLine.Y2 = end.Y;
            gameCanvas.Children.Add(projectileLine);
        }

        public List<Rect> GenerateHitBoxes()
        {
            List<Rect> hitBoxes = new List<Rect>();
            double distance = Point.Subtract(end, start).Length;
            Vector direction = (end - start) / distance;

            int numHitBoxes = (int)(distance / 10);
            double halfThickness = thickness / 2.0;

            for (int i = 0; i < numHitBoxes; i++)
            {
                Point hitboxCenter = start + direction * (i * 10 + 10 / 2);
                Rect hitbox = new Rect(hitboxCenter.X - halfThickness, hitboxCenter.Y - halfThickness, thickness, thickness);
                hitBoxes.Add(hitbox);
            }

            return hitBoxes;
        }

        private bool Collision(Rectangle target)
        {
            foreach (Rect hitbox in hitBoxes)
            {
                if (hitbox.IntersectsWith(new Rect(Canvas.GetLeft(target), Canvas.GetTop(target), target.Width, target.Height)))
                {
                    this.end.X = hitbox.X + hitbox.Width / 2;
                    this.end.Y = hitbox.Y + hitbox.Height / 2;
                    return true;
                }
            }
            return false;
        }

        private bool CollisionCheckEnemy(Rect bulletHitbox, List<Enemy> enemies)
        {   
                foreach(Enemy target in enemies)
                {
                    Rect enemyHitbox = new Rect(Canvas.GetLeft(target.enemyImage), Canvas.GetTop(target.enemyImage), target.enemyImage.Width, target.enemyImage.Height);
                    if (bulletHitbox.IntersectsWith(enemyHitbox)){
                        // update pozycji konca linii strzalu
                        this.end.X = bulletHitbox.X + bulletHitbox.Width / 2;
                        this.end.Y = bulletHitbox.Y + bulletHitbox.Height / 2;
                        // deal damage to the target and if its health == 0 remove it from the canvas
                        if (target.IsDead(damage))
                        {
                            //gameCanvas.Children.Remove(target.enemyImage);
                            //enemies.Remove(target);
                        }
                        return true;
                    }
                }
            return false;
        }

        /*
        private bool CollisionWithEnemy(Enemy target)
        {
            if (target.enemyImage != null && hitBoxes.Count > 0)
            {
                foreach (Rect hitbox in hitBoxes)
                {
                    if (hitbox.IntersectsWith(new Rect(Canvas.GetLeft(target.enemyImage), Canvas.GetTop(target.enemyImage), target.enemyImage.Width, target.enemyImage.Height)))
                    {
                        target.HoldMove(gameCanvas);
                        DispatcherTimer hitVisualizaionTimer = new DispatcherTimer();
                        hitVisualizaionTimer.Interval = TimeSpan.FromMilliseconds(200);
                        bool tick = false;

                        double y = Canvas.GetTop(target.enemyImage);
                        double x = Canvas.GetLeft(target.enemyImage);

                        // funkcja matiego do knockbacku dla przeciwnikow
                        hitVisualizaionTimer.Tick += (sender, e) =>
                        {
                            if (!tick)
                            {
                                target.enemyImage.Opacity = 0.4;
                                tick = true;
                            }
                            else
                            {
                                target.enemyImage.Opacity = 1;
                                tick = false;
                                hitVisualizaionTimer.Stop();
                            }
                        };
                        hitVisualizaionTimer.Start();

                        // deal damage to the target and if its health == 0 remove it from the canvas
                        if (target.IsDead(damage))
                        {
                            gameCanvas.Children.Remove(target.enemyImage);
                            hitBoxes.Remove(hitbox);
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        */
    }
}
