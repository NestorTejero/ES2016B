using System.Runtime.CompilerServices;

public class ScoreManager : Manager
{
    public ScoreManager()
    {
        value = 0;
        APIHUD.instance.setPoints("0");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public new void Add(int val)
    {
        base.Add(val);
        APIHUD.instance.setPoints(value.ToString());
    }

    public new void AddDeadUnit(Unit unit)
    {
        base.AddDeadUnit(unit);
        APIHUD.instance.setPoints(value.ToString());
    }

    public void WriteFinalScore()
    {
        //
    }
}