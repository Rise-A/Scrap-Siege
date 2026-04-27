using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleWeaponScript : MonoBehaviour
{
    public GameObject bottleProjectile;
    public GameObject bottleWeapon;
    public Transform LaunchPoint;
    public float bottleLife;
    public float bottleCooldown = 1f;
    bool readyToThrowBottle;
    public float bottleVelocity = 700f;

    void Start(){
        readyToThrowBottle = true;
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && readyToThrowBottle){
            readyToThrowBottle = false;
            ThrowBottle();
            bottleWeapon.SetActive(false);
            Invoke(nameof(ResetBottle), bottleCooldown);

        }
    }

    private void ThrowBottle(){
        GameObject bottle = Instantiate(bottleProjectile, LaunchPoint.position, LaunchPoint.rotation);
        bottleProjectile.SetActive(true);
        bottle.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, bottleVelocity, 0));

        Destroy(bottle, bottleLife);
    }
    
    private void ResetBottle(){
        bottleWeapon.SetActive(true);
        readyToThrowBottle = true;
    }
}
