using System.Runtime.CompilerServices;
using UnityEngine;

public class Manager : MonoBehaviour
{
    protected readonly int difficulty;
    protected int value;

    public Manager()
    {
        value = 0;
        difficulty = PersistentValues.difficulty;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(int val)
    {
        value += val*difficulty;
    }

    public void AddDeadUnit(Unit unit)
    {
        Add(unit.rewardCoins*difficulty);
    }
}