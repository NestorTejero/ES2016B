using System.Runtime.CompilerServices;

public class MoneyManager : Manager
{
    public MoneyManager(int initMoney)
    {
        Add(initMoney);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public new void Add(int val)
    {
        base.Add(val);
    }

    public void Spend(int val)
    {
        if (value - val < 0)
            Add(-value);
        else
            Add(-val);
    }

    public int GetNumCoins()
    {
        return value;
    }
}