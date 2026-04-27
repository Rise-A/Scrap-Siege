using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSwitching : MonoBehaviour
{
    [Header("Weapon Information")]
    public GameObject Bottle;
    public GameObject Pistol;
    public GameObject RocketLauncher;
    public GameObject Minigun;
    public GameObject Player;
    GameObject currentWeapon;
    GameObject previousWeapon;
    public int pistolCost;
    public int rocketLauncherCost;
    public int minigunCost;
    int selectedItem = 0;

    [Header("References")]
    public PlayerScript playerScript;
    public GameObject Shop;
    public ShopScript shopScript;
    // Start is called before the first frame update
    void Start()
    {
        // SelectItem();
        shopScript = Shop.GetComponent<ShopScript>();
        playerScript = Player.GetComponent<PlayerScript>();

        currentWeapon = Bottle;
        Pistol.SetActive(false);
        RocketLauncher.SetActive(false);
        Minigun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // int previousSelectedItem = selectedItem;

        // if (Input.GetAxis("Mouse ScrollWheel") > 0f){
        //     if (selectedItem >= transform.childCount - 1){
        //         selectedItem = 0;
        //     }
        //     else{
        //         selectedItem++;
        //     }
        // }

        // if (Input.GetAxis("Mouse ScrollWheel") < 0f){
        //     if (selectedItem <= 0){
        //         selectedItem = transform.childCount - 1;
        //     }
        //     else{
        //         selectedItem--;
        //     }
        // }

        // if (previousSelectedItem != selectedItem){
        //     SelectItem();
        // }
    }

    public void upgradeToPistol(){
        if (playerScript.buildingMaterials >= pistolCost && currentWeapon == Bottle){
            shopScript.CloseShop();
            playerScript.buildingMaterials -= pistolCost;
            previousWeapon = Bottle;
            currentWeapon = Pistol;
            previousWeapon.SetActive(false);
            currentWeapon.SetActive(true);
        }

    }

    public void upgradeToRocketLauncher(){
        if (playerScript.buildingMaterials >= rocketLauncherCost && currentWeapon == Pistol){
            shopScript.CloseShop();
            playerScript.buildingMaterials -= rocketLauncherCost;
            previousWeapon = Pistol;
            currentWeapon = RocketLauncher;
            previousWeapon.SetActive(false);
            currentWeapon.SetActive(true);
        }
    }

    public void upgradeToMinigun(){
        if (playerScript.buildingMaterials >= minigunCost && currentWeapon == RocketLauncher){
            shopScript.CloseShop();
            playerScript.buildingMaterials -= minigunCost;
            previousWeapon = RocketLauncher;
            currentWeapon = Minigun;
            previousWeapon.SetActive(false);
            currentWeapon.SetActive(true);
        }
    }

    // void SelectItem(){
    //     int i = 0;
    //     foreach (Transform item in transform){
    //         if (i == selectedItem){
    //             item.gameObject.SetActive(true);
    //         }
    //         else{
    //             item.gameObject.SetActive(false);
    //         }
    //         i++;
    //     }
    // }
}
