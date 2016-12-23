using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, CanUpgrade, CanReceiveDamage, HUDSubject
{
    public float baseHealth;
    private int currentLevel;

    public HealthComponent health;
    private int maxLevel;

    //level values min = 1, max = 3
    private int minLevel;
    public float repairCost;
    public float repairQuantity;
    private MeshRenderer skin;
    private ParticleSystem smoke;
    private GameObject smokeEffect;

    private GameObject textureModel;
    //textures to apply on each level
    public List<Texture> textures;
    public float upgradeCost;
    public float upgradeFactor;
    public AudioSource upgrade,repair;

    private BuildingWeapon weapon;

    // Receive damage by weapon
    public void ReceiveDamage(float damage)
    {
        try
        {
            health.LoseHealth(damage);
            NotifyHUD();
            ApplyMainTexture();
            ApplySmokeEffect();
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
        return (upgradeCost <= numCoins) && (currentLevel < maxLevel);
    }

    // To upgrade when there are enough coins
    public void Upgrade()
    {
        if (!IsUpgradeable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().GetNumCoins()))
            return;
        GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().SpendCoins((int) upgradeCost);
        health.Upgrade(upgradeFactor);
        weapon.Upgrade();
        weapon.setProjectile(currentLevel);
        currentLevel++;
        NotifyHUD();
        ApplyMainTexture();
        ApplySmokeEffect();
        if (!upgrade.isPlaying)
            upgrade.PlayOneShot(upgrade.clip);
        Debug.Log("BUILDING UPGRADED, TOTAL HP: " + health.GetTotalHealth());
    }

    public void NotifyHUD()
    {
        var updateInfo = new HUDInfo
        {
            CurrentHealth = health.GetCurrentHealth(),
            TotalHealth = health.GetTotalHealth(),
            VisibleUpgradeButton =
                IsUpgradeable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().GetNumCoins()),
            VisibleRepairButton =
                IsRepairable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().GetNumCoins())
        };

        APIHUD.instance.notifyChange(this, updateInfo);
    }

    //To check if have enough money to buy the tower
    public bool canBuild()
    {
        var currentMoney = GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().GetNumCoins();
        var towerCost = GameObject.Find("Towers").GetComponentInChildren<Tower>().buildCost;
        if (currentMoney >= towerCost)
            return true;
        Debug.Log("Not enough money to build");
        return false;
    }

    //To spend the money in order to pay for the tower
    public void buyTower()
    {
        var towerCost = GameObject.Find("Towers").GetComponentInChildren<Tower>().buildCost;
        GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().SpendCoins(towerCost);
    }

    // Use this for initialization
    private void Start()
    {
        minLevel = 1;
        maxLevel = 3;
        currentLevel = minLevel;

        health = new HealthComponent(baseHealth);
        Debug.Log("BUILDING CREATED with HP: " + baseHealth);

        textureModel = transform.FindChild("Model").gameObject;
        skin = textureModel.GetComponent<MeshRenderer>();
        skin.material.mainTexture = textures[(int) UBTextureHPIndex.Full];

        //particle data
        smokeEffect = transform.FindChild("WhiteSmoke").gameObject;
        smoke = transform.GetComponent<ParticleSystem>();
        smokeEffect.SetActive(false);

        weapon = gameObject.GetComponent<BuildingWeapon>();
    }

    private void Update()
    {
        // Shot on mouseclick
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200))
            {
                var target = hit.transform.gameObject;
                if (target.tag == "Unit")
                {
                    var unit = target.GetComponent<CanReceiveDamage>();
                    weapon.addTarget(unit);
                    weapon.Attack();
                    weapon.removeTarget(unit);
                }
                else
                {
                    weapon.ShootAt(hit.point);
                }
            }
        }
    }

    public bool IsRepairable(int numCoins)
    {
        return (repairCost <= numCoins) && !health.IsHealthFull();
    }

    // Repair the building
    public void Repair()
    {
        if (!IsRepairable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().GetNumCoins()))
            return;
        GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().SpendCoins((int) repairCost);
        health.AddHealth(repairQuantity);
        NotifyHUD();
        ApplyMainTexture();
        ApplySmokeEffect();
        if (!repair.isPlaying)
            repair.PlayOneShot(repair.clip);
    }

    /*
     * Applies texture to ub depending on its Health and Level
     * 
     */

    public void ApplyMainTexture()
    {
        var hp = health.GetCurrentHealthPercentage();
        var text = skin.material.mainTexture; // to avoid null values

        if (hp > (float) HPThreshold.Medium)
            text = textures[3*(currentLevel - 1) + (int) UBTextureHPIndex.Full];
        else if ((hp > (float) HPThreshold.Low) && (hp < (float) HPThreshold.Medium))
            text = textures[3*(currentLevel - 1) + (int) UBTextureHPIndex.Medium];
        else
            text = textures[3*(currentLevel - 1) + (int) UBTextureHPIndex.Low];

        skin.material.mainTexture = text;
    }

    private void ApplySmokeEffect()
    {
        var hp = health.GetCurrentHealthPercentage();
        if (hp >= (float) HPThreshold.Medium)
            smokeEffect.SetActive(false);
        else //if (hp >= (float)HPThreshold.Low && hp < (float)HPThreshold.Medium)
            smokeEffect.SetActive(true);
    }

    private enum UBTextureHPIndex
    {
        Full,
        Medium,
        Low
    }

    private enum HPThreshold
    {
        Full = 100,
        Medium = 50,
        Low = 25
    }
}