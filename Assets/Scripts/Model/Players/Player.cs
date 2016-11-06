using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected List<Unit> availableUnits;
    public int initialCoins;
    protected int numCoins, unitsWave;

    public abstract void Play();
	public abstract void PlayAI();

    private void Start()
    {
        numCoins = initialCoins;
		unitsWave = 0;
        InvokeRepeating("Play", 3.0f, 2.0f);
		InvokeRepeating("PlayAI",15.0f, 2.0f);
    }

    public void addCoins(int coins)
    {
        numCoins += coins;
    }

    public void getMoney(Unit deadUnit)
    {
        Debug.Log("Oh! I've received " + deadUnit.rewardCoins + " coins! :D yay");
        addCoins(deadUnit.rewardCoins);
    }

	/* Give human player time to upgrade or repair */
	public void timeToUpgrade(){
		CancelInvoke (methodName: "PlayAI");
		float delay = 15.0f;
		while (delay > 0) {
			delay -= Time.deltaTime;
		}
		InvokeRepeating("PlayAI",15.0f, 2.0f);
	}
		
}