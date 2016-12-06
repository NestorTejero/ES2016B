using System.Runtime.CompilerServices;

public class ScoreManager
{
    private int score;
    private readonly int difficulty;

    public ScoreManager()
    {
        score = 0;
        APIHUD.instance.setPoints("0");
        difficulty = PersistentValues.difficulty;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(int val)
    {
        score += val*difficulty;
        APIHUD.instance.setPoints(score.ToString());
    }

    public void WriteFinalScore()
    {
        //
    }
}