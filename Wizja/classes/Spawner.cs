using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wizja.Enemies;
using System.Windows.Shapes;
using System.Diagnostics.CodeAnalysis;

namespace Wizja.classes;
public class Spawner
{
    private int frequency;
    private List<SpawnerObject> enemiesSpawner = new List<SpawnerObject>(); // Dajemy pozycje wszystkich puntków spawnujących
    private List<Enemy>[] enemies; //Przetrzymuje przeciwnków
    private int rounds;
    private int betweenRounds;
    private int currentRound = 0;
    private int tickCount = 0;
    private Canvas gameScreen;
    public Spawner(int frequency, List<Point> enemiesSpawner, int rounds,int betweenRounds,Canvas gameScreen)
    {
        this.rounds= rounds;
        this.frequency=frequency;
        this.enemies = new List<Enemy>[rounds];
        this.betweenRounds = betweenRounds;
        this.gameScreen = gameScreen;
        foreach (Point place in enemiesSpawner)
        {
            this.enemiesSpawner.Add(new SpawnerObject(gameScreen, place));
        }
    }
    public void GetEnemies(List<int>[] enemyLists)
    {
        for (int i = 0; i < rounds; i++)
        {
            enemies[i] = new List<Enemy> { };
            foreach (int enemyID in enemyLists[i])
            {
                switch (enemyID) 
                {
                    case 1:
                        enemies[i].Add(new SlowZombie());
                        break;
                    case 2:
                        enemies[i].Add(new FastZombie());
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }
        }
    }
    public void Spawn() 
    {
        if (currentRound >= rounds)
        {
            return;
        }
        if (tickCount % frequency == 0 && enemies[currentRound].Count > tickCount / frequency)
        {
            Enemy temp = enemies[currentRound].ElementAt(tickCount / frequency);
            Rectangle obj = temp.enemyImage;
            Canvas.SetLeft(obj, Canvas.GetLeft(enemiesSpawner[2].place));
            Canvas.SetTop(obj, Canvas.GetTop(enemiesSpawner[2].place));
            gameScreen.Children.Add(obj);
            temp.isLiving = true;
        }
        tickCount++;
        if (tickCount % betweenRounds == 0 && currentRound < rounds)
        {
            currentRound++;
            tickCount = 0;
        }
    }
    public void MoveEveryOne(Rectangle player ) 
    {
        for (int i = 0; i < rounds; i++)
        {
            foreach (Enemy enemy in enemies[i]) 
            {
                if (enemy.isLiving) 
                {
                    enemy.Follow(player);
                }
            }
        }
    }

}
