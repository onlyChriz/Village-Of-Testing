namespace Village_of_testing;

public class Worker
{
    public string Name { get; set; }
    public bool Hungry { get; set; }
    public int DaysHungry { get; set; }
    
    public string WorkTitle { get; set; }
    public bool IsAlive { get; set; }

    public delegate void Occupation();

    private Occupation WorkTask;
    
    public Worker(string name ,string worktitle ,Occupation work)
    {
        Name = name;
        Hungry = false;
        WorkTitle = worktitle;
        DaysHungry = 0;
        IsAlive = true;
        WorkTask = work;
        
    }
    public void DoWork()
    {
        if (Hungry == false)
        {
            WorkTask();
        }
    }
    
}