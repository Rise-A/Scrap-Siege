using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI timeAliveText;
    public EnemySpawner enemySpawnerScript;
    public GameObject enemySpawner;
    public TowerScript towerScript;
    public GameObject towerHolder;
    public bool gameOverScreenActive;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreenActive = false;
        gameOverScreen.SetActive(false);
        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawner>();
        towerScript = towerHolder.GetComponent<TowerScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = enemySpawnerScript.numEnemiesKilled.ToString();
        timeAliveText.text = towerScript.finalTime.ToString();

        if (gameOverScreenActive){
            OpenScreen();
        }
    }

    public void OpenScreen(){
        gameOverScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
