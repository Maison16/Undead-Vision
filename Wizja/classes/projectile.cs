using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Wizja.Enemies;
using T = System.Timers;

namespace Wizja.classes
{
    public class Projectile
    {
        private Point start;
        private Point end;
        private double thickness;
        private List<Rect> hitBoxes;
        private Canvas gameCanvas;
        private Line projectileLine;
        private double range;
        private T.Timer timer;

        public Projectile(double startX, double startY, Vector direction, double thickness, double range, int damage, List<Rectangle> mapObjects, List<Enemy> enemies, Canvas gameCanvas)
        {
            this.start = new Point(startX, startY);
            this.thickness = thickness;
            this.gameCanvas = gameCanvas;
            this.range = range;

            this.end = new Point(start.X + direction.X * range, start.Y + direction.Y * range);

            this.hitBoxes = GenerateHitBoxes();
            DrawLine();
            foreach (Rectangle target in mapObjects)
            {
                UpdateEndWithCollision(target);
            }
            foreach (Enemy target in enemies)
            {
                UpdateEndWithCollision(target.enemyImage);
                if (target.enemyImage != null && hitBoxes.Count > 0)
                {
                    foreach (Rect hitbox in hitBoxes)
                    {
                        if (hitbox.IntersectsWith(new Rect(Canvas.GetLeft(target.enemyImage), Canvas.GetTop(target.enemyImage), target.enemyImage.Width, target.enemyImage.Height)))
                        {
                            if (target.IsDead(damage))
                            {
                                gameCanvas.Children.Remove(target.enemyImage);
                            }

                            break;
                        }
                    }
                }
            }

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
            projectileLine.Stroke = Brushes.LightGoldenrodYellow;
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

        private void UpdateEndWithCollision(Rectangle target)
        {
            foreach (Rect hitbox in hitBoxes)
            {
                if (hitbox.IntersectsWith(new Rect(Canvas.GetLeft(target), Canvas.GetTop(target), target.Width, target.Height)))
                {
                    end = new Point(hitbox.X + hitbox.Width / 2, hitbox.Y + hitbox.Height / 2);
                    projectileLine.X2 = end.X;
                    projectileLine.Y2 = end.Y;
                    break;
                }
            }
        }
    }
}
