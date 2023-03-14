using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using static Village_of_testing.Worker;
namespace Village_of_testing;

public class Village
{
    #region Properties

    public int Food { get; set; }
    public int Wood { get; set; }
    public int Metal { get; set; }
    public int FoodPerDay { get;} = 5;
    public int WoodPerDay { get;} = 1;
    public int MetalPerDay { get;} = 1;
    public int DaysGone { get; set; } = 0;

    public int WorkerSlots { get; set; }

    #endregion

    #region Lists
    public List<Worker> Workers { get; } = new List<Worker>();
    public List<Building> Buildings { get; } = new List<Building>();
    public List<Building> Projects { get; } = new List<Building>();

    #endregion

    #region Building Creations

        public Building House = new ("House", 5, 0, 0,3,false);
        public Building Woodmill = new ("Woodmill", 5, 1, 0,5,false);
        public Building Quarry = new ("Quarry", 3, 5, 0,5,false);
        public Building Farm = new ("Farm", 5, 2, 0,5,false);
        public Building Castle = new ("Castle", 50, 50, 0,50,false);

    #endregion

    #region Constructors

    public Village()
    {
        Start();
    }
    public Village(int wood, int metal, int food)
    {
        Buildings.Add(House);
        Food = food;
        Wood = wood;
        Metal = metal;
        WorkerSlots = Housing();
    }

    public Village(DatabaseConnection databaseConnection) // mocking
    {
        DatabaseConnection = databaseConnection;
        Start();
    }

    public Village(RandomNumberGenerator generator)
    {
        RandomNumberGenerator = generator;
        Start();
    }
    #endregion

    public RandomNumberGenerator RandomNumberGenerator { get; set; } = new();
    public DatabaseConnection DatabaseConnection { get; set; } = new();

    public void Start()
    {
        Building beginnerHouse = new Building("House", 5, 0, 3,3,true);
        Buildings.Add(beginnerHouse);
        Buildings.Add(beginnerHouse);
        Buildings.Add(beginnerHouse);
        Food = 10;
        WorkerSlots = Housing();
    }
    

    public int GetFood() { return Food; }
    public int GetWood() { return Wood; }
    public int GetMetal() { return Metal; }
    public int GetWorkers() { return Workers.Count; }
    public string FindWorker(string name)
    {
        foreach (var work in Workers)
        {
            if (work.WorkTitle == name)
            {
                return work.WorkTitle;
            }
            
        }

        return "";
    }

    public string SetOcupation(Occupation work)
    {
        if (work == Build)
        {
            return "Builder";
        }
        if (work == AddFood)
        {
            return "Farmer";
        }
        if (work == AddWood)
        {
            return "Lumberjack";
        }
        if (work == AddMetal)
        {
            return "Miner";
        }

        return "";
    }

    public int Housing()
    {
        int sum = 0;
        foreach (var building in Buildings)
        {
            if (building.Name == House.Name)
            {
                sum++;
            }
        }
        return sum * 2;
    }

    public void AddWorker(string name, Occupation work)
    {
        if (WorkerSlots > 0)
        {
            var worker = new Worker(name, SetOcupation(work) ,work);
            Workers.Add(worker);
            WorkerSlots--;
        }
        return;
    }
    
    public void AddProject(Building building)
    {
        Console.WriteLine($"Metal: {Metal} | Wood: {Wood} | MetalCost: {building.MetalCost} | WoodCost: {building.WoodCost}");
        if (Metal >= building.MetalCost && Wood >= building.WoodCost)
        {
            Console.WriteLine($"Added {building.Name} to project list");
            Projects.Add(building);

            Wood -= building.WoodCost;
            Metal -= building.MetalCost;
        }
        else
            Console.WriteLine($"insufficient materials   | Required material\n" +
                              $" Wood: {Wood}                 |          Wood: {building.WoodCost}\n" +
                              $"Metal: {Metal}                 |         Metal: {building.MetalCost}\n");
    }

    public void Day()
    {
        DaysGone++;
        if(Workers.Count > 0)
        {
            FeedWorkers();
            foreach (var worker in Workers)
            {
                worker.DoWork();
            }
        }

    }

    #region Work Tasks

    public void AddFood()
    {
        Food += MaterialsLoop(Buildings, "Farm", FoodPerDay, 10);
    }

    public void AddWood()
    {
        Wood += MaterialsLoop(Buildings, "Woodmill", WoodPerDay, 2);
    }

    public void AddMetal()
    {
        Metal += MaterialsLoop(Buildings, "Quarry", MetalPerDay, 2);
    }
    
    public void Build()
    {
        if (Projects.Count > 0)
        {
            Projects[0].DaysWorkerOn++;

            foreach (Building building in Projects.ToList())
            {
                if (building.DaysWorkerOn == building.DaysToComplete)
                {
                    building.Complete = true;
                    Buildings.Add(building);
                    Projects.Remove(building);
                }
            }
        }
    }

    #endregion

    #region Mocking

    public void RandomWorker(RandomNumberGenerator generator)
        {
            switch (generator.RandomNumber())
            {
                case 0: AddWorker("kalle",Build);
                    break;
                case 1:AddWorker("kalle",AddFood);
                    break;
                case 2:AddWorker("kalle",AddWood);
                    break;
                case 3:AddWorker("kalle",AddMetal);
                    break;
            }
        }

    public Village LoadForMocking()
    {
        DatabaseConnection.Load(this);
        return this;
    }

    #endregion
    


    public void FeedWorkers()
    {
        for (int i = 0; i < Workers.Count; i++)
        {
            if(Food>0)
            {
                Food--;
                Workers[i].Hungry = false;
            }
            if(Workers[i].DaysHungry > 39)
            {
                Workers[i].IsAlive = false;
                BuryDead();
            }
            else if (Food == 0)
            {
                Workers[i].Hungry = true;
                Workers[i].DaysHungry++;
            }
            
        }
    }

    public void BuryDead()
    {
        for (int i = 0; i < Workers.Count; i++)
        {
            if (Workers[i].IsAlive == false)
            {
                Console.WriteLine($"buried worker {Workers[i].Name}");
                Workers.Remove(Workers[i]);
            }
            
        }
    }
    
    private int MaterialsLoop(List<Building> buildings, string buildingName, int material, int perDay)
    {
        
        foreach (var xBuilding in buildings)
        {
            if (xBuilding.Name == buildingName)
            {
                material += perDay;
                
            }
        }

        return material;
    }
    
}