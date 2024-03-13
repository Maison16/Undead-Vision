using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wizja.Enemies;

namespace Wizja.classes;
public class Spawner
{
    private int[] frequency; //Częstotliwość między spawnem przeciwników, w każdej rundzie może być inna
    private List<SpawnerObject> enemiesSpawner = new List<SpawnerObject>(); // Dajemy pozycje wszystkich puntków spawnujących
    public List<Enemy>[] enemies; //Przetrzymuje przeciwnków
    private int rounds;//Liczba rund
    private int enemyCurrentNumber = 0;
    public int currentRound = 0;
    private int tickCount = 0;
    private Canvas gameScreen;
    private Canvas upperCanvas;
    private Player player;



    public Spawner(List<Point> enemiesSpawner, int rounds, Canvas gameScreen, Player player, Canvas upperCanvas)
    {
        this.upperCanvas = upperCanvas;
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
            else if (value <= wave[0] + wave[1] + wave[2]+ wave[3])
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
                enemies[noRound].Add(new SlowZombie(upperCanvas));
                break;
            case 2:
                enemies[noRound].Add(new Wolf(upperCanvas));
                break;
            case 3:
                enemies[noRound].Add(new TankSkeleton(upperCanvas));
                break;
            case 4:
                enemies[noRound].Add(new ShootingZombie(upperCanvas));
                break;


        }
    }

    //Spawnuje przeciwników
    public void Spawn()
    {
        player.gameTick++;
        if (currentRound >= rounds)
        {
            return;
        }
        if (tickCount % frequency[currentRound] == 0)
        {
            Enemy temp;
            Rectangle obj;
            Rectangle pointer;
            foreach (SpawnerObject spawnerObj in TheClosestSpawners())
            {
                if (enemies[currentRound].Count() <= enemyCurrentNumber)
                {
                    break;
                }
                temp = enemies[currentRound].ElementAt(enemyCurrentNumber);
                obj = temp.enemyImage;
                pointer = temp.pointer;
                Canvas.SetLeft(obj, Canvas.GetLeft(spawnerObj.place));
                Canvas.SetTop(obj, Canvas.GetTop(spawnerObj.place));
                Canvas.SetLeft(pointer, Canvas.GetLeft(spawnerObj.place)+temp.enemyImage.Width / 2);
                Canvas.SetTop(pointer, Canvas.GetTop(spawnerObj.place) + temp.enemyImage.Height / 2);
                gameScreen.Children.Add(obj);
                upperCanvas.Children.Add(pointer);
                temp.isLiving = true;
                enemyCurrentNumber++;

                temp.timerBlackout.Start();
            }
        }
        tickCount++;
        if (AllDead() && currentRound <= rounds)
        {
            player.hud.SetTime(30);
            enemyCurrentNumber = 0;
            currentRound++;
            tickCount = 0;
        }
    }
    //Przesuwa wszystkie potwory i sprawdza czy mogą one zatakować gracza
    public void MoveEveryOne(Player player, List<Rectangle> mapObject)
    {
        for (int i = 0; i < rounds; i++)
        {
            foreach (Enemy enemy in enemies[i])
            {
                enemy.SetPlayer(player);
                Rectangle playerImage = player.playerImage;

                if (enemy.isLiving)
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
    public bool AllDead()
    {
        if (currentRound >= rounds) 
        {
            return true;
        }
        foreach (Enemy en in enemies[currentRound])
        {
            if (en.isLiving)
                return false;
        }
        return true;
    }
    private Rectangle ColisionWithBulding(Enemy enemy, List<Rectangle> mapObject)
    {
        if (enemy.GetType().ToString().Contains("ShootingZombie"))
        {
            return player.playerImage;
        }
        foreach (Rectangle rectangle in mapObject)
        {
            double hg = rectangle.Height;
            if (rectangle.Height > 130)
                hg -= 60;
            else if (rectangle.Height > 40)
                hg -= 30;
            Rect building = new Rect(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle), rectangle.Width-30, hg);
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
        SpawnerObject maxSpawnerObj = enemiesSpawner.ElementAt(0);
        double maxDistance = maxSpawnerObj.DistanceToPlayer(player.playerImage);
        foreach (SpawnerObject spawnerObj in enemiesSpawner.Skip(1))
        {
            double distance = spawnerObj.DistanceToPlayer(player.playerImage);
            if (maxDistance > distance)
            {
                result.Add(maxSpawnerObj);
                maxSpawnerObj = spawnerObj;
                maxDistance = distance;
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
    public int EnemyCount(int i)
    {
       return enemies[i].Count();
    }
}
