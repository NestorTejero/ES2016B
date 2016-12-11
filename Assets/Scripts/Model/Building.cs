using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, CanUpgrade, CanReceiveDamage, HUDSubject
{
    enum UBTexture {Level1FullHp,Level1MediumHp,Level1LowHp, Level2FullHp, Level2MediumHp, Level2LowHp, Level3FullHp, Level3MediumHp, Level3LowHp }
    enum HPThreshold  { Full = 100,Medium = 50,Low = 25} 
    public float baseHealth;

    public HealthComponent health;
    public float repairCost;
    public float repairQuantity;
    public float upgradeCost;
    public float upgradeFactor;

    //level values min = 1, max = 3
    private int minLevel;
    private int currentLevel;
    private int maxLevel;


    private GameObject textureModel;
    private MeshRenderer skin;
    //textures to apply on each level
    public List<Texture> textures;

    // Receive damage by weapon
    public void ReceiveDamage(float damage)
    {
        try
        {
            health.LoseHealth(damage);
            NotifyHUD();
            Debug.Log("BUILDING RECEIVED DAMAGE: " + damage + " - CURRENT_HEALTH: " + health.GetCurrentHealth());
            ApplyMainTexture();
        }
        catch (Exception)
        {
            NotifyHUD();
            GameController.instance.notifyDeath(this);
        }
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public bool IsUpgradeable(int numCoins)
    {
        
        return (upgradeCost <= numCoins) & (currentLevel < maxLevel);

    }

    // To upgrade when there are enough coins
    public void Upgrade()
    {
        GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().SpendCoins((int)upgradeCost);
        health.Upgrade(upgradeFactor);
        currentLevel++;
        NotifyHUD();
        ApplyMainTexture();
        Debug.Log("BUILDING UPGRADED, TOTAL HP: " + health.GetTotalHealth());
    }

	//To check if have enough money to buy the tower
	public bool canBuild(){
		int currentMoney = GameObject.FindGameObjectWithTag ("Human").GetComponent<Player> ().GetNumCoins ();
		int towerCost = GameObject.Find ("Towers").GetComponentInChildren<Tower> ().buildCost;
		if (currentMoney >= towerCost) {
			Debug.Log("You have money to build");
			return true;
		} else {
			return false;
		}
	}

	//To spend the money in order to pay for the tower
	public void buyTower(){
		int towerCost = GameObject.Find ("Towers").GetComponentInChildren<Tower> ().buildCost;
		GameObject.FindGameObjectWithTag ("Human").GetComponent<Player> ().SpendCoins (towerCost);
	}

    public void NotifyHUD()
    {
        var updateInfo = new HUDInfo
        {
            CurrentHealth = health.GetCurrentHealth(),
            TotalHealth = health.GetTotalHealth(),
			VisibleUpgradeButton = IsUpgradeable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().GetNumCoins()),
			VisibleRepairButton = IsRepairable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().GetNumCoins())
        };

        APIHUD.instance.notifyChange(this, updateInfo);
    }

    // Use this for initialization
    private void Start()
    {
        minLevel = 1;
        maxLevel = 3;
        currentLevel = minLevel;

        health = new HealthComponent(baseHealth);
        Debug.Log("BUILDING CREATED with HP: " + baseHealth);


        textureModel = this.transform.FindChild("Model").gameObject;
        skin = textureModel.GetComponent<MeshRenderer>();
        skin.material.mainTexture = textures[(int)UBTexture.Level1FullHp];
    }

    public bool IsRepairable(int numCoins)
    {
        return (repairCost <= numCoins) && !health.IsHealthFull();
    }

    // Repair the building
    public void Repair()
    {
        GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().SpendCoins((int)repairCost);
        health.AddHealth(repairQuantity);
        NotifyHUD();
        ApplyMainTexture();
        Debug.Log("BUILDING REPAIRED, CURRENT HP: " + health.GetCurrentHealth());
    }

    /*
     * Applies texture to ub depending on its Health and Level
     * 
     */
    public void ApplyMainTexture()
    {
        var hp = health.GetCurrentHealthPercentage();
        var text = textures[0]; // to avoid null values
        switch (currentLevel)
        {
            case 1:
                if (hp > (float)HPThreshold.Medium)
                {
                    text = textures[(int)UBTexture.Level1FullHp];
                }
                else if(hp > (float)HPThreshold.Low && hp < (float)HPThreshold.Medium)
                {
                    text = textures[(int)UBTexture.Level1MediumHp];
                }
                else
                {
                    text = textures[(int)UBTexture.Level1LowHp];
                }
                    
                break;

            case 2:
                if (hp > (float)HPThreshold.Medium)
                {
                    text = textures[(int)UBTexture.Level2FullHp];
                }
                else if (hp > (float)HPThreshold.Low && hp < (float)HPThreshold.Medium)
                {
                    text = textures[(int)UBTexture.Level2MediumHp];
                }
                else
                {
                    text = textures[(int)UBTexture.Level2LowHp];
                }
                break;
            case 3:
                if (hp > (float)HPThreshold.Medium)
                {
                    text = textures[(int)UBTexture.Level3FullHp];
                }
                else if (hp > (float)HPThreshold.Low && hp < (float)HPThreshold.Medium)
                {
                    text = textures[(int)UBTexture.Level3MediumHp];
                }
                else
                {
                    text = textures[(int)UBTexture.Level3LowHp];
                }
                break;

            default: break;

        }
        skin.material.mainTexture = text;

    }
}