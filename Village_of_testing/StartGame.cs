using System.Drawing;

namespace Village_of_testing;

public class StartGame
{
    public Village village = new Village();
    
    

    public void Run()
    {

        do
        {
            
            ListForActions();
            if (village.GetFood() == 0 && village.GetWorkers() == 0)
            {
                Console.Clear();
                Console.WriteLine($"YOU LOSE\nIt took {village.DaysGone} days to LOSE");
                PlayAgain();
            }
        } while (!village.Buildings.Contains(village.Castle));
        
        
        Console.Clear();
        Console.WriteLine($"YOU WIN\nIt took {village.DaysGone} days to WIN");
        PlayAgain();
    }

    public bool PlayAgain()
    {
        Console.WriteLine("Play again?");
        while (true)
        {
            var yesNo = Console.ReadKey(true);

            switch (yesNo.Key)
            {
                case ConsoleKey.Y:
                    village = new Village();
                    return true;
                case ConsoleKey.N:
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Good bye.. don't forget to come back soon.");
                    Console.ResetColor();
                    return false;
            }
        }
        
    }
    public void ListForActions()
    {
        Console.Clear();
        GameWindow();
        Console.WriteLine("1. Project-list if you have a builder.\n2. Add a worker.\n3. Skip a day");
        var input = int.Parse(Console.ReadLine());
        switch (input)
        {
            case 1: Console.Clear();
                Console.WriteLine("1. House\n2. Farm\n3. Woodmill\n4. Quarry\n5.Castle");
                ProjectsList(int.Parse(Console.ReadLine()));
                break;
            case 2: Console.Clear();
                Console.WriteLine("1. Builder\n2. Farmer\n3. Forester\n4. Miner");
                AddWorker(int.Parse(Console.ReadLine()));
                break;
            case 3: Console.Clear();
                Console.WriteLine("How Many days do you want to skip");
                SkipDays(int.Parse(Console.ReadLine()));
                break;
            default: Console.WriteLine();
                break;
        }
    }

    public void ProjectsList(int input)
    {
        Console.Clear();
        GameWindow();
        if (village.FindWorker("Builder") == "Builder")
        {
            switch (input)
            {
                case 1: village.AddProject(village.House);
                    break;
                case 2: village.AddProject(village.Farm);
                    break;
                case 3: village.AddProject(village.Woodmill);
                    break;
                case 4: village.AddProject(village.Quarry);
                    break;
                case 5: village.AddProject(village.Castle);
                    break;
                default: Console.WriteLine();
                    break;
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("no builder in your work group\npress any key to continue:");
            Console.ReadKey();

        }
    }
    
    public void AddWorker(int input)
    {
        
        switch (input)
        {
            case 1: village.AddWorker("ola", village.Build);
                Run();
                break;
            case 2: village.AddWorker("kalle", village.AddFood);
                Run();
                break;
            case 3: village.AddWorker("peter", village.AddWood);
                Run();
                break;
            case 4: village.AddWorker("bert", village.AddMetal);
                Run();
                break;
            default: Console.WriteLine();
                break;
        }
    }

    
    public int TotalWorker(string worktitle)
    {
        int sum = 0;
        foreach (var work in village.Workers)
        {
            if (work.WorkTitle == worktitle)
            {
                sum++;
            }
            
        }

        return sum;
    }

    public int TotalBuilding(string name)
    {
        int sum = 0;
        foreach (var building in village.Buildings)
        {
            if (building.Name == name)
            {
                sum++;
            }
            
        }

        return sum;
    }
    
    public void GameWindow()
    {

        Console.WriteLine("\n" +
                          $"    Food {village.GetFood()}           Wood: {village.GetWood()}             Metal: {village.GetMetal()}");
        Console.WriteLine($"\n    builders: {TotalWorker("Builder")}                      House: {TotalBuilding("House")}\n" +
                          $"      Farmer: {TotalWorker("Farmer")}                       Farm: {TotalBuilding("Farm")}\n" +
                          $"  Lumberjack: {TotalWorker("Lumberjack")}                   Woodmill: {TotalBuilding("Woodmill")}\n" +
                          $"       Miner: {TotalWorker("Miner")}                     Quarry: {TotalBuilding("Quarry")}\n" +
                          $"Worker slots: {village.WorkerSlots}                  Days gone: {village.DaysGone}\n");
        
    }

    public void SkipDays(int x)
    {
        for (int i = 0; i < x; i++)
        {
            village.Day();
        }
    }
    
    
}