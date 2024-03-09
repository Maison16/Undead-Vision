using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizja.classes;
public class Spawner
{
    public int frequency;
    List<Rectangle> EnemiesSpawner; // Dajemy pozycje wszystkich puntków spawnujących
    Spawner(int frequency, List<Rectangle> enemiesSpawner)
    {
        this.frequency=frequency;
        this.EnemiesSpawner=enemiesSpawner;
    }
}
