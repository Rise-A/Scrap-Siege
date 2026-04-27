using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public GameObject ShopUI;
    public TowerScript ts;
    public ItemSwitching itsw;
    public GameObject towerHolder;
    public GameObject itemHolder;
    public TextMeshProUGUI boxPrice;
    public TextMeshProUGUI towerPrice;
    public TextMeshProUGUI spirePrice;
    public TextMeshProUGUI pistolPrice;
    public TextMeshProUGUI rocketLauncherPrice;
    public TextMeshProUGUI minigunPrice;
    Boolean shopIsOpen;
    // Start is called before the first frame update
    void Start()
    {
        shopIsOpen = false;
        ts = towerHolder.GetComponent<TowerScript>();
        itsw = itemHolder.GetComponent<ItemSwitching>();
        boxPrice.text = ts.upgradeToBoxCost.ToString();
        towerPrice.text = ts.upgradeToTowerCost.ToString();
        spirePrice.text = ts.upgradeToSpireCost.ToString();

        pistolPrice.text = itsw.pistolCost.ToString();
        rocketLauncherPrice.text = itsw.rocketLauncherCost.ToString();
        minigunPrice.text = itsw.minigunCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (shopIsOpen == false){
            ShopUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R)){
            OpenShop();
        }

        if (Input.GetKeyDown(KeyCode.T)){
            CloseShop();
        }

    }

    public void OpenShop(){
        ShopUI.SetActive(true);
        shopIsOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseShop(){
        ShopUI.SetActive(false);
        shopIsOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
