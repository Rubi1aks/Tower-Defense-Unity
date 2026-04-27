using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class Gamecontorller : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject standartturret;
    public GameObject missaLauncher;
    public GameObject laserBeam;
    public GameObject prefabImp;

    public int towerCost;
    public int turretCost = 15;
    public int LoncherCost = 135;
    public int LaserCost = 75;

    public Renderer baseRenderer;
    public Text GameoverText;

    public int baseHP = 5;

    [SerializeField]
    Text moneText;
    public int BaseHP
    {
        get { return baseHP;  }
        set 
        {
            if (baseRenderer)
            {
                baseHP = value;
                baseRenderer.material.color = Color.HSVToRGB(1, 1, baseHP / 5.0f);
                if (baseHP <= 0)
                {
                    print("fmwefgsw");
                    GameOver();

                }
            }
        }
    }

    private void GameOver()
    {
        Destroy(baseRenderer);
        GameObject impact = Instantiate(prefabImp, baseRenderer.transform.position, Quaternion.identity);
        Destroy(impact, 4f);
        GameoverText.enabled = true;
        
        Spawner spawner = GetComponent<Spawner>();
        spawner.gameOver = true;
    }

    public int money = 50;
    private void Start()
    {
        baseRenderer = baseRenderer.GetComponent<Renderer>();
        Money = 50;
        SetStandartTurret();
        GameoverText.enabled = false;
    }
    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            moneText.text = "$" + money;
        }
    } 
    public void LaserBeam()
    {
        TowerPrefab = laserBeam;
        towerCost = LaserCost;
    }
    public void SetStandartTurret()
    {
        TowerPrefab = standartturret;
        towerCost = turretCost;
    } 
    public void SetMissle()
    {
        TowerPrefab = missaLauncher;
        towerCost = LoncherCost;
    }
}
