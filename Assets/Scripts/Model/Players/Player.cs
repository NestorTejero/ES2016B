using UnityEngine;

public class Player : MonoBehaviour
{
    public int initialCoins;
    protected int numCoins, unitsWave;
    private ScoreManager score;

    private void Start()
    {
        numCoins = initialCoins;
        APIHUD.instance.setMoney(numCoins.ToString());
        score = new ScoreManager();
    }

    public void AddCoins(int coins)
    {
        numCoins += coins;
        APIHUD.instance.setMoney(numCoins.ToString());
    }

    public void SpendCoins(int coins)
    {
        numCoins -= coins;
        if (numCoins < 0)
            numCoins = 0;
        APIHUD.instance.setMoney(numCoins.ToString());
		MoneyTextManager.Instance.CreateText (GameObject.Find("Grid+Camera").transform.FindChild("Grid").transform.position,coins.ToString(),false);
    }

    public void GetMoney(Unit deadUnit)
    {
        Debug.Log("Oh! I've received " + deadUnit.rewardCoins + " coins! :D yay");
        AddCoins(deadUnit.rewardCoins);
        score.Add(deadUnit.rewardCoins);
		MoneyTextManager.Instance.CreateText (GameObject.Find("Grid+Camera").transform.FindChild("Grid").transform.position,deadUnit.rewardCoins.ToString(),true);
    }

    public int GetNumCoins()
    {
        return numCoins;
    }

    public void ChangeWave()
    {
        score.Add(1000);
        AddCoins(1000);
    }
}