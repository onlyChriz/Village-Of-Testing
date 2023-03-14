namespace Village_of_testing;

public class DatabaseConnection
{

    public void Save()
    {
        
    }

    public void Load(Village village)
    {
        village.Metal = GetMetal();
    }

    public virtual int GetMetal()
    {
        return 20;
    }
}