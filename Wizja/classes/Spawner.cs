using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Wizja.Enemies;

namespace Wizja.classes;
public class Spawner
{
    private int[] frequency; //Częstotliwość między spawnem przeciwników, w każdej rundzie może być inna
    private List<SpawnerObject> enemiesSpawner = new List<SpawnerObject>(); // Dajemy pozycje wszystkich puntków spawnujących
    private List<Enemy>[] enemies; //Przetrzymuje przeciwnków
    private int rounds;//Liczba rund
    private int enemyCurrentNumber = 0;
    private int currentRound = 0;
    private int tickCount = 0;
    private Canvas gameScreen;
    private Player player;


    public Spawner(List<Point> enemiesSpawner, int rounds, Canvas gameScreen, Player player)
    {
        this.rounds = rounds;
        frequency = new int[rounds];
        this.enemies = new List<Enemy>[rounds];
        this.player = player;
        this.gameScreen = gameScreen;
        foreach (Point place in enemiesSpawner)
        {
            this.enemiesSpawner.Add(new SpawnerObject(gameScreen, place));
        }
    }

    //Generuje losowych przeciwników na podstawie pradwopodobieństwa
    public void GenerateEnemies(int[] wave, int numbeOfEnemies, int noRound, int fr)
    {

        Random rnd = new Random();
        int value;
        frequency[noRound] = fr;
        enemies[noRound] = new List<Enemy>();
        for (int i = 0; i < numbeOfEnemies; i++)
        {
            value = rnd.Next(0, 100);
            if (value <= wave[0])
            {
                GetEnemies(1, noRound);
            }
            else if (value <= wave[0] + wave[1])
            {
                GetEnemies(2, noRound);
            }
            else if (value <= wave[0] + wave[1] + wave[2])
            {
                GetEnemies(3, noRound);
            }
            else if (value <= wave[0] + wave[1] + wave[3])
            {
                GetEnemies(4, noRound);
            }
        }
    }

    //Dodaje tych przeciwników do listy
    public void GetEnemies(int enemyID, int noRound)
    {
        switch (enemyID)
        {
            case 1:
                enemies[noRound].Add(new SlowZombie());
                break;
            case 2:
                enemies[noRound].Add(new Wolf());
                break;
            case 3:
                enemies[noRound].Add(new TankSkeleton());
                break;
            case 4:
                enemies[noRound].Add(new ShootingZombie());
                break;


        }
    }

    //Spawnuje przeciwników
    public void Spawn()
    {
        if (currentRound >= rounds)
        {
            return;
        }
        if (tickCount % frequency[currentRound] == 0)
        {
            Enemy temp;
            Rectangle obj;
            foreach (SpawnerObject spawnerObj in TheClosestSpawners())
            {
                if (enemies[currentRound].Count() <= enemyCurrentNumber)
                {
                    break;
                }
                temp = enemies[currentRound].ElementAt(enemyCurrentNumber);
                obj = temp.enemyImage;
                Canvas.SetLeft(obj, Canvas.GetLeft(spawnerObj.place));
                Canvas.SetTop(obj, Canvas.GetTop(spawnerObj.place));
                gameScreen.Children.Add(obj);
                temp.isLiving = true;
                enemyCurrentNumber++;
            }
        }
        tickCount++;
        if (AllDead() && currentRound < rounds)
        {
            player.hud.SetTime(30);
            enemyCurrentNumber = 0;
            currentRound++;
            tickCount = 0;
        }
    }

    //Przesuwa wszystkie potwory i sprawdza czy mogą one zatakować gracza
    public void MoveEveryOne(Player player, List <Rectangle> mapObject)
    {
        for (int i = 0; i < rounds; i++)
        {
            foreach (Enemy enemy in enemies[i])
            {
                Rectangle playerImage = player.playerImage;

                if (enemy.isLiving )
                {
                    Rect hitbox = new Rect(Canvas.GetLeft(playerImage), Canvas.GetTop(playerImage), playerImage.Width, playerImage.Height);
                    Rectangle rec = ColisionWithBulding(enemy, mapObject);
                    if (!playerImage.Equals(rec))
                    {
                        enemy.BreakCollision(rec, gameScreen, playerImage);
                    }
                    else 
                    {
                        enemy.Follow(playerImage, gameScreen);
                    }
                    if (enemy.IsColision(hitbox))
                    {
                        player.TakeDamage(enemy.DealDamage());
                    }

                }
            }
        }
    }
    private bool AllDead() 
    {
        foreach (Enemy en in enemies[currentRound]) 
        {
            if(en.isLiving)
            return false;
        }
        return true;
    }
    private Rectangle ColisionWithBulding(Enemy enemy,List<Rectangle> mapObject) 
    {
        foreach (Rectangle rectangle in mapObject)
        {
            Rect building = new Rect(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle), rectangle.Width, rectangle.Height);
            if (enemy.IsColision(building))
            {
                return rectangle;
            }
        }
        return player.playerImage;
    } 
    //Sprawdza który z spawnerów są najbliżej playera i powinny spawnować przeciwników
    public List<SpawnerObject> TheClosestSpawners()
    {
        List<SpawnerObject> result = new List<SpawnerObject>();
        SpawnerObject maxSpawnerObj = enemiesSpawner.ElementAt(1);
        double maxDistance = maxSpawnerObj.DistanceToPlayer(player.playerImage);
        foreach (SpawnerObject spawnerObj in enemiesSpawner.Skip(1))
        {
            if (maxDistance < spawnerObj.DistanceToPlayer(player.playerImage))
            {
                result.Add(maxSpawnerObj);
                maxSpawnerObj = spawnerObj;
            }
            else
            {
                result.Add(spawnerObj);
            }
        }
        return result;
    }

    public List<Enemy> GetAllEnemies()
    {
        List<Enemy> allEnemies = new List<Enemy>();

        foreach (List<Enemy> enemyList in enemies)
        {
            allEnemies.AddRange(enemyList);
        }

        return allEnemies;
    }
}
