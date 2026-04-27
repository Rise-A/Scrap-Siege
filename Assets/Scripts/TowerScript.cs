using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    [Header("Tower Information")]
    public GameObject Lv0Ground;
    public int upgradeToBoxCost;
    public GameObject Lv1Box;
    public int upgradeToTowerCost;
    public GameObject Lv2Tower;
    public int upgradeToSpireCost;
    public GameObject Lv3Spire;
    public GameObject Player;
    public Rigidbody playerRigidBody;
    public TextMeshProUGUI healthText;

    [Header("Misc Info")]
    public int playerMaxHealth;
    int playerCurrentHealth;
    public int boxMaxHealth;
    int boxCurrentHealth;
    public int towerMaxHealth;
    int towerCurrentHealth;
    public int spireMaxHealth;
    int spireCurrentHealth;
    int currentBuildingMaxHealth;
    int currentBuildingHealth;
    GameObject currentBuilding;
    GameObject previousBuilding;
    Boolean canUpgradeTower;
    Boolean isUpgrading;
    public float timeElapsed;
    public float finalTime;
    

    [Header("References")]
    public PlayerScript playerScript;
    public GameObject Shop;
    public ShopScript shopScript;
    public HealthBarScript healthBar;
    public GameOverScreen gameOverScreenScript;
    public GameObject gameOverScreenObject;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        Lv1Box.SetActive(false);
        Lv2Tower.SetActive(false);
        Lv3Spire.SetActive(false);
        isUpgrading = false;

        playerScript = Player.GetComponent<PlayerScript>();
        shopScript = Shop.GetComponent<ShopScript>();
        gameOverScreenScript = gameOverScreenObject.GetComponent<GameOverScreen>();
        
        currentBuilding = Lv0Ground;
        currentBuildingHealth = playerMaxHealth;
        boxCurrentHealth = boxMaxHealth;
        towerCurrentHealth = towerMaxHealth;
        spireCurrentHealth = spireMaxHealth;
        healthBar.setMaxHealth(playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        healthText.text = currentBuildingHealth.ToString();
        //Debug.Log(currentBuildingHealth);

        // Destroying Tower
        if (currentBuilding == Lv0Ground && currentBuildingHealth <= 0){
            GameOver();
        }

        if (currentBuilding == Lv1Box && currentBuildingHealth <= 0){
            currentBuildingHealth = playerMaxHealth;
            downgradeToGround();
        }

        if (currentBuilding == Lv2Tower && currentBuildingHealth <= 0){
            currentBuildingHealth = boxMaxHealth;
            downgradeToBox();
        }

        if (currentBuilding == Lv3Spire && currentBuildingHealth <= 0){
            currentBuildingHealth = spireMaxHealth;
            downgradeToTower();
        }

        if (Input.GetKeyDown(KeyCode.G)){
            TakeDamage(20);
        }
    }

    IEnumerator TowerUpgradeTimer()
    {
        yield return new WaitForSeconds(1);
        currentBuilding.SetActive(true);
    }

    IEnumerator PreviousTowerDisableTimer() {
        yield return new WaitForSeconds(1);
        previousBuilding.SetActive(false);
    }

    void launchPlayerDuringUpgrade(){
        int upgradeLaunchForce = 10;

        playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, 0f, playerRigidBody.velocity.z);
        playerRigidBody.AddForce(transform.up * upgradeLaunchForce, ForceMode.Impulse);
    }

    // Methods to Upgrade Tower
    public void upgradeToBox(){
        if (playerScript.buildingMaterials >= upgradeToBoxCost && currentBuilding == Lv0Ground){
            healthBar.setMaxHealth(boxMaxHealth);
            healthBar.setHealth(boxMaxHealth);
            shopScript.CloseShop();
            playerScript.buildingMaterials -= upgradeToBoxCost;
            currentBuildingHealth = boxMaxHealth;
            previousBuilding = Lv0Ground;
            currentBuilding = Lv1Box;
            StartCoroutine(PreviousTowerDisableTimer());
            launchPlayerDuringUpgrade();
            StartCoroutine(TowerUpgradeTimer());
        }
    }

    public void upgradeToTower(){
        if (playerScript.buildingMaterials >= upgradeToTowerCost && currentBuilding == Lv1Box){
            healthBar.setMaxHealth(towerMaxHealth);
            healthBar.setHealth(towerMaxHealth);
            shopScript.CloseShop();
            playerScript.buildingMaterials -= upgradeToTowerCost;
            currentBuildingHealth = towerMaxHealth;
            previousBuilding = Lv1Box;
            currentBuilding = Lv2Tower;
            StartCoroutine(PreviousTowerDisableTimer());
            launchPlayerDuringUpgrade();
            StartCoroutine(TowerUpgradeTimer());
        }
    }

    public void upgradeToSpire(){
        if (playerScript.buildingMaterials >= upgradeToSpireCost && currentBuilding == Lv2Tower){
            healthBar.setMaxHealth(spireMaxHealth);
            healthBar.setHealth(spireMaxHealth);
            shopScript.CloseShop();
            playerScript.buildingMaterials -= upgradeToSpireCost;
            currentBuildingHealth = spireMaxHealth;
            previousBuilding = Lv2Tower;
            currentBuilding = Lv3Spire;
            StartCoroutine(PreviousTowerDisableTimer());
            launchPlayerDuringUpgrade();
            StartCoroutine(TowerUpgradeTimer());
        }
    }

    // Methods to Downgrade Tower
    void downgradeToTower(){
        previousBuilding = Lv3Spire;
        currentBuilding = Lv2Tower;
        healthBar.setMaxHealth(towerMaxHealth);
        healthBar.setHealth(towerMaxHealth);
        currentBuilding.SetActive(true);
        previousBuilding.SetActive(false);
    }

    void downgradeToBox(){
        previousBuilding = Lv2Tower;
        currentBuilding = Lv1Box;
        healthBar.setMaxHealth(boxMaxHealth);
        healthBar.setHealth(boxMaxHealth);
        currentBuilding.SetActive(true);
        previousBuilding.SetActive(false);
    }

    void downgradeToGround(){
        previousBuilding = Lv1Box;
        currentBuilding = Lv0Ground;
        healthBar.setMaxHealth(playerMaxHealth);
        healthBar.setHealth(playerMaxHealth);
        currentBuilding.SetActive(true);
        previousBuilding.SetActive(false);
    }

    public void TakeDamage(int damage){
        currentBuildingHealth = currentBuildingHealth - damage;
        healthBar.setHealth(currentBuildingHealth);
        
        // if (currentBuilding == Lv0Ground){
        //     healthBar.fillAmount = currentBuildingHealth / playerMaxHealth;
        // }

        // if (currentBuilding == Lv1Box){
        //     healthBar.fillAmount = currentBuildingHealth / boxMaxHealth;
        // }

        // if (currentBuilding == Lv2Tower){
        //     healthBar.fillAmount = currentBuildingHealth / towerMaxHealth;
        // }

        // if (currentBuilding == Lv3Spire){
        //     healthBar.fillAmount = currentBuildingHealth / spireMaxHealth;
        // }
    }

    public void GameOver(){
        finalTime = timeElapsed;
        gameOverScreenScript.OpenScreen();        
        Time.timeScale = 0;
    }
}
