using UnityEngine;
using System.Collections;

public abstract class AI : Player
{
    public abstract void Play();

    protected void Start()
    {
        numCoins = initialCoins;
        unitsWave = 0;
        InvokeRepeating("Play", 15.0f, 2.0f);
        InvokeRepeating("AutoCoins", 0.0f, 1.0f);
    }

    /* Give human player time to upgrade or repair */

    public void timeToUpgrade()
    {
        CancelInvoke(methodName: "Play");
        CancelInvoke(methodName: "AutoCoins");
        float delay = 15.0f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        InvokeRepeating("Play", 10.0f, 2.0f);
        InvokeRepeating("AutoCoins", 0.0f, 1.0f);
    }

    protected void AutoCoins()
    {
        numCoins += 5;
    }
}