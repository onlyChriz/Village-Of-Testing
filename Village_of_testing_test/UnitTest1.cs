using System.Reflection.Metadata;
using Moq;
using Village_of_testing;
using Xunit.Abstractions;

namespace Village_of_testing_test;


public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    Village village2 = new Village(50,50,50);
    Village village = new Village();


    
    [Fact]
    public void Hunger_Death_Test()
    {
        village.AddWorker("peter", village.Build);
        village.AddWorker("peter", village.Build);
        village.AddWorker("peter", village.Build);

        village.Food = 0;
        Day_Iteretion_Loop(village,41);
        
        var expected = 0;
        
        Assert.Equal(expected,village.Workers.Count);

    }
    [Fact]
    public void Building_Done()
    {
        
        village2.AddWorker("kalle", village2.Build);
        village2.AddProject(village2.House);
        Day_Iteretion_Loop(village2,4);

        var expected = 2;

        Assert.Equal(expected, village2.Buildings.Count);
    }
    [Fact]
    public void Add_Worker_test()
    {
        village.AddWorker("billie",village.Build);
        Add_Worker_Loop(village,2, village.Build);
        Add_Worker_Loop(village, 3, village.Build);

        var expected = 6;
        Assert.Equal(expected, village.Workers.Count);
    }
    [Fact]
    public void Add_Worker_No_Houses()
    {
        Add_Worker_Loop(village,10,village.AddMetal);

        var expected = village.Housing();
        
        Assert.Equal(expected,village.Workers.Count);
    }

    [Fact]
    public void Add_Worker_DoWork()
    {
        village.AddWorker("klas", village.AddMetal);
        village.Day();

        var exp = 1;
        
        Assert.Equal(exp, village.GetMetal());
    }

    [Fact]
    public void Run_Day_Without_Worker()
    {
        Day_Iteretion_Loop(village,10);

        var exp = 10;
        
        Assert.Equal(11,village.DaysGone);
        Assert.Equal(exp,village.Food);
    }

    [Fact]
    public void Run_Day_With_Workers()
    {
        village.AddWorker("per",village.AddMetal);
        Day_Iteretion_Loop(village,3);
        
        var foodExp = 10 - 4;
        
        Assert.Equal(4,village.DaysGone);
        Assert.Equal(foodExp,village.GetFood());
    }

    [Fact]
    public void To_Little_Food()
    {
        var food = village.Food = 0;
        
        village.AddWorker("olle", village.AddMetal);
        Day_Iteretion_Loop(village,10);
        var metalExp = 0;
        
        Assert.Equal(metalExp, village.Metal);
        Assert.Equal(11,village.DaysGone);
    }

    #region Building from Start to finnish

    [Fact]
    public void House_Building_Testing()
    {
        village2.AddWorker("ola", village2.Build);
        village2.AddProject(village2.House);
        Day_Iteretion_Loop(village2,7);
        
        
        Assert.Equal(50-village2.House.MetalCost,village2.GetMetal());
        Assert.Equal(50-village2.House.WoodCost,village2.GetWood());
    }
    [Fact]
    public void Farm_Building_Testing()
    {
        
        village2.AddWorker("ola", village2.Build);
        village2.AddProject(village2.Farm);
        Day_Iteretion_Loop(village2,7);
        
        
        Assert.Equal(50-village2.Farm.MetalCost,village2.GetMetal());
        Assert.Equal(50-village2.Farm.WoodCost,village2.GetWood());
    }
    [Fact]
    public void Woodmill_Building_Testing()
    {
        village2.AddWorker("ola", village2.Build);
        village2.AddProject(village2.Woodmill);
        Day_Iteretion_Loop(village2,7);
        
        
        Assert.Equal(50-village2.Woodmill.MetalCost,village2.GetMetal());
        Assert.Equal(50-village2.Woodmill.WoodCost,village2.GetWood());
    }
    [Fact]
    public void Quarry_Building_Testing()
    {
        village2.AddWorker("ola", village2.Build);
        village2.AddProject(village2.Quarry);
        Day_Iteretion_Loop(village2,7);
        
        
        Assert.Equal(50-village2.Quarry.MetalCost,village2.GetMetal());
        Assert.Equal(50-village2.Quarry.WoodCost,village2.GetWood());
    }
    [Fact]
    public void Castle_building_testing()
    {
        village2.AddWorker("ola", village2.Build);
        village2.AddProject(village2.Castle);
        Day_Iteretion_Loop(village2,50);
        
        
        Assert.Equal(50-village2.Castle.MetalCost,village2.GetMetal());
        Assert.Equal(50-village2.Castle.WoodCost,village2.GetWood());
    }


    #endregion
    
    [Fact]
    public void Add_Building_Without_Resourses()
    {
        village.AddWorker("peter",village.Build);
        village.AddProject(village.Castle);

        var exp = 3;
        
        Assert.Equal(exp,village.Buildings.Count);
        
    }

    [Fact]
    public void Wood_One_Day_Before_Woodmill_And_One_Day_After()
    {
        var expWood = 51;
        var expMill = 60;
        village2.AddWorker("per",village2.AddWood);
        village2.AddWorker("kalle",village2.Build);
        village2.Day();
        Assert.Equal(expWood,village2.Wood);
        
        village2.AddProject(village2.Woodmill);
        Day_Iteretion_Loop(village2,6);
        
        village2.Day();
        Assert.Equal(expMill,village2.Wood);

    }
    
    [Fact]
    public void Food_One_Day_Before_Farm_And_One_Day_After()
    {
        var expFood = 54;
        var expFarm = 95;
        village2.AddWorker("per",village2.AddFood);
        village2.Day();
        Assert.Equal(expFood,village2.GetFood());
        
        village2.AddWorker("kalle",village2.Build);
        village2.AddProject(village2.Farm);

        Day_Iteretion_Loop(village2,village2.Farm.DaysToComplete);
        village2.Day();
        
        Assert.Equal(2,village2.Buildings.Count);
        Assert.Equal(expFarm,village2.GetFood());
    }
    
    [Fact]
    public void Metal_One_Day_Before_Mine_And_One_Day_After()
    {
        Village village3 = new Village(50,50,50);

        var expMetal = 51;
        var expQuarry = 56;
        village3.AddWorker("kalle",village3.Build);
        village3.AddWorker("per",village3.AddMetal);
        village3.Day();
        Assert.Equal(expMetal,village3.GetMetal());
        
        
        village3.AddProject(village3.Quarry);

        Day_Iteretion_Loop(village3,4);
        village3.Day();
        
        Assert.Equal(2,village3.Buildings.Count);
        Assert.Equal(expQuarry,village3.GetMetal());
    }

    [Fact]
    public void Days_It_Takes_For_Buildings_With_Day()
    {
        village2.AddWorker("ola", village2.Build);
        village2.AddProject(village2.House);
        village2.Day();
        village2.AddWorker("ola", village2.Build);
        village2.Day();
        
        int expDays = 2;
        int expBuildings = 2;
        
        Assert.Equal(expBuildings, village2.Buildings.Count);
        Assert.Equal(expDays,village2.DaysGone);
        

    }
    
    // test for worker is done in hunger death test

    [Fact]
    public void Test_From_Start_To_Castle()
    {
        var village1 = new Village();
        
        village1.AddWorker("kalle", village1.AddFood);
        village1.AddWorker("kalle", village1.AddMetal);
        village1.AddWorker("kalle", village1.AddWood);
        village1.AddWorker("kalle", village1.Build);
        
        
        Day_Iteretion_Loop(village1,49);
        Assert.Equal(50,village1.Wood);
        Assert.Equal(50,village1.Metal);
        village1.AddProject(village1.Castle);
        Day_Iteretion_Loop(village1,49);

        Assert.Contains(village1.Castle, village1.Buildings);
    }


    #region Mocking

    [Fact]
    public void Mocking_Load_From_DB()
    {
        MockRepository mockRepository = new MockRepository(MockBehavior.Default);
        var mockDatabaseConnection = mockRepository.Create<DatabaseConnection>();
        DatabaseConnection mocking = mockDatabaseConnection.Object;
        var villageMock = new Village(mocking);
        mockDatabaseConnection.Setup(mock => mock.GetMetal()).Returns(20);

        Village actual = villageMock.LoadForMocking();
        
        Assert.True(actual != null);
        Assert.Equal(20,actual.Metal);
    }

    [Fact]
    public void Random_Worker_Mock_Test()
    {
        Mock<RandomNumberGenerator> mock = new Mock<RandomNumberGenerator>();
        RandomNumberGenerator generator = mock.Object;
        var villageMockWorker = new Village(generator);

        mock.Setup(mock => mock.RandomNumber()).Returns(1);
        villageMockWorker.RandomWorker(villageMockWorker.RandomNumberGenerator);
        
        villageMockWorker.Day();

        var expectedFood = 14;
        var actualFood = villageMockWorker.Food;

        Assert.Single(villageMockWorker.Workers);
        Assert.Equal(expectedFood,actualFood);
    }
    #endregion
    #region Methods for testing

    public void Day_Iteretion_Loop(Village vill, int iteration)
    {
        for (int i = 0; i <= iteration; i++)
        {
            vill.Day();
        }
    }

    public void Add_Worker_Loop(Village village, int iteration, Worker.Occupation occupation)
    {
        for (int i = 0; i < iteration; i++)
        {
            this.village.AddWorker($"per{i}", occupation);
        }
    }

    #endregion
    
}
