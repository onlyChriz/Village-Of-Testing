namespace Village_of_testing;

public class RandomNumberGenerator
{
    public virtual int RandomNumber()
    {
        Random random = new Random();
        int rand = random.Next(0, 4);

        return rand;
    }
}