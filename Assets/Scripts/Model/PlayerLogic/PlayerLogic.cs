public abstract class PlayerLogic
{
    private int numCoins;

    public PlayerLogic()
    {
        this.numCoins = 0;
    }

    public abstract void Play();

    public void addCoins(int coins)
    {
        numCoins += coins;
    }
}