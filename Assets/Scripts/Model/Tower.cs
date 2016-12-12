using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour, CanUpgrade, HUDSubject
{
    private int minLevel;
    private int currentLevel;
    private int maxLevel;

    public int upgradeCost;
    private Weapon weapon;
	public int buildCost;
    public AudioSource source;
    public AudioClip buy, upgrade;

    private GameObject model;
    private MeshRenderer skin;
    public List<Texture> textures;

    public bool IsUpgradeable(int numCoins)
    {
        return (upgradeCost <= numCoins ) && (currentLevel < maxLevel);
    }

    // To upgrade when there are enough coins
    public void Upgrade()
    {
        if (!IsUpgradeable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().GetNumCoins()))
            return;
        GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().SpendCoins((int)upgradeCost);
        weapon.Upgrade();
        currentLevel++;
		weapon.setProjectile (currentLevel - 1);
		NotifyHUD();
        ApplyMainModelScale();
        ApplyMainTexture();
        //Sound
        if (!source.isPlaying)
            source.PlayOneShot(upgrade);

        Debug.Log("TOWER UPGRADED, Power: " + weapon.getCurrentDamage());
    }

    public void NotifyHUD()
    {
        var updateInfo = new HUDInfo
        {
            Damage = weapon.getCurrentDamage().ToString(),
            Range = weapon.getCurrentRange().ToString(),
			VisibleUpgradeButton = IsUpgradeable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().GetNumCoins())
        };

        APIHUD.instance.notifyChange(this, updateInfo);
    }

    // Use this for initialization
    private void Start()
    {
        minLevel = 1;
        maxLevel = 3;
        currentLevel = minLevel;
        weapon = gameObject.GetComponent<Weapon>();
        //Put towermodel on scale of 0.5 for first level
        model = transform.FindChild("TowerModel").gameObject;
        model.transform.localScale = new Vector3(model.transform.localScale.x, model.transform.localScale.y, 0.5f);
        //texture data
        skin = model.GetComponent<MeshRenderer>();
        skin.material.mainTexture = textures[currentLevel - 1];
        //Sound
        if (!source.isPlaying)
            source.PlayOneShot(buy);
        Debug.Log("TOWER CREATED");
    }

    // If enemy enters the range of attack
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            // Adds enemy to attack to the queue
            weapon.addTarget(col.gameObject.GetComponentInParent<CanReceiveDamage>());
        }
    }

    // If enemy exits the range of attack
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
            weapon.removeTarget(col.gameObject.GetComponentInParent<CanReceiveDamage>());
    }

    private void ApplyMainModelScale()
    {
        switch (currentLevel)
        {
            case 1:
                model.transform.localScale = new Vector3(model.transform.localScale.x, model.transform.localScale.y, 0.5f);
                break;
            case 2:
                model.transform.localScale = new Vector3(model.transform.localScale.x, model.transform.localScale.y, 0.8f);
                break;
            case 3:
                model.transform.localScale = new Vector3(model.transform.localScale.x, model.transform.localScale.y, 1.0f);
                break;
            default: break;
        }
    }

    public void ApplyMainTexture()
    {
        skin.material.mainTexture = textures[currentLevel - 1];
    }
}
