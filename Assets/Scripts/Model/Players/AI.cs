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
    }

    /* Give human player time to upgrade or repair */
    public void timeToUpgrade()
    {
        CancelInvoke(methodName: "Play");
        float delay = 15.0f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        InvokeRepeating("Play", 15.0f, 2.0f);
    }
}
