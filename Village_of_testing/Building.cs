namespace Village_of_testing;

public class Building
{
    public string Name { get; set; }
    public int WoodCost { get; set; }
    public int MetalCost { get; set; }
    public int DaysWorkerOn { get; set; }
    public int DaysToComplete { get; set; }
    public bool Complete { get; set; }

    

    public Building(string name, int woodCost, int metalCost, int daysWorkerOn, int daysToComplete, bool complete)
    {
        Name = name;
        WoodCost = woodCost;
        MetalCost = metalCost;
        DaysWorkerOn = daysWorkerOn;
        DaysToComplete = daysToComplete;
        Complete = complete;
    }
    
}