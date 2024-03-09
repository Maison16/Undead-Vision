using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using T = System.Timers;

namespace Wizja.classes
{
    public class Projectile
    {
        private Point start;
        private Point end;
        private double thickness;
        private Rectangle target; // podmienic potem na liste przeciwnikow
        private List<Rect> hitBoxes;
        private Canvas gameCanvas;
        private Line projectileLine;
        T.Timer timer;

        // Rectangle target do podmianki na liste
        public Projectile(double startX, double startY, double endX, double endY, double thickness, Rectangle target, Canvas gameCanvas)
        {
            this.start = new Point(startX, startY);
            this.end = new Point(endX, endY);
            this.thickness = thickness;
            this.gameCanvas = gameCanvas;

            this.hitBoxes = GenerateHitBoxes(); // generowanie hitboxow w zdluz linii
            DrawLine(); // rysuj linie
            UpdateEndWithCollision(target); // aktualizuj dane do narysowania linii po sprawdzeniu kolizji  

            // Timer do usuniencia linii po pewnym czasie (potencjalnie mozna dodac do konstruktora jako custom zmienna)
            timer = new T.Timer();
            timer.Interval = 95; // milliseconds
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = false; // Only fire once
            timer.Start();
        }

        private void OnTimerElapsed(object sender, EventArgs e)
        {
            // Usun z canvasu po uplywie czasu
            gameCanvas.Dispatcher.Invoke(() =>
            {
                gameCanvas.Children.Remove(projectileLine);
            });
        }

        public void DrawLine()
        {
            projectileLine = new Line();
            projectileLine.Stroke = Brushes.Black; // kolor lini
            projectileLine.StrokeThickness = thickness; // grubos lini (wybrana w konstruktorze)
            projectileLine.X1 = start.X; // x startu
            projectileLine.Y1 = start.Y; // y startu
            projectileLine.X2 = end.X; // x konca
            projectileLine.Y2 = end.Y; // y konca
            gameCanvas.Children.Add(projectileLine); // dodanie lini na canvas
        }

        public List<Rect> GenerateHitBoxes()
        {
            List<Rect> hitBoxes = new List<Rect>();
            double distance = Point.Subtract(end, start).Length;
            Vector direction = (end - start) / distance;

            // obliczenie ile hitboxow na linie
            int numHitBoxes = (int)(distance / 10);
            double halfThickness = thickness / 2.0;

            // generowanie hitboxow w zdluz lini
            for (int i = 0; i < numHitBoxes; i++)
            {
                Point hitboxCenter = start + direction * (i * 10 + 10 / 2);
                Rect hitbox = new Rect(hitboxCenter.X - halfThickness, hitboxCenter.Y - halfThickness, thickness, thickness);
                hitBoxes.Add(hitbox);
            }

            return hitBoxes;
        }


        // sprawdzanie najblizszej kolizji od gracza i zwrorcenie x,y takowej kolizji (poniewaz hitboxy sa dodawana od gracza do konca linii)
        private void UpdateEndWithCollision(Rectangle target)
        {
            // Check for collision with each hitbox
            foreach (Rect hitbox in hitBoxes)
            {
                if (hitbox.IntersectsWith(new Rect(Canvas.GetLeft(target), Canvas.GetTop(target), target.Width, target.Height)))
                {
                    // Update the end point to the intersection point
                    end = new Point(hitbox.X + hitbox.Width / 2, hitbox.Y + hitbox.Height / 2);
                    // Update the line with the new end point
                    projectileLine.X2 = end.X;
                    projectileLine.Y2 = end.Y;
                    this.target = target;
                    break; // Exit the loop after the first collision
                }
            }
        }
    }
}
