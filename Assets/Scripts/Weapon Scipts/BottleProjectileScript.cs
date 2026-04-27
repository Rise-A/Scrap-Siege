using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleProjectileScript : MonoBehaviour
{
    public GameObject bottleProjectile;
    public GameObject brokenBottle;
    public float bottleBreakForce = 200;
    public int damage = 100;
    // public float bottleBreakPowerMax = 1000;
    // public float bottleBreakRadius = 1000;

    public AudioSource bottleThrowSound;
    bool hasCollided;

    void Start(){
        // bottleThrowSound = GetComponent<AudioSource>();
        // bottleThrowSound.Play();
    }

    void Update(){
        transform.position = bottleProjectile.transform.position;
        // for arrows VV
        //transform.LookAt(transform.position + bottle.GetComponent<Rigidbody>().velocity/2);
        
        transform.Rotate(Random.Range(-3f, -1f), Random.Range(-2f, 2f), 0, Space.Self);

    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Ground"){
            Destroy(bottleProjectile);
            BreakBottle();
        }

        Gobbler gobbler = collision.gameObject.GetComponent<Gobbler>(); // Get the Gobbler component from the collision object

        if (gobbler != null){
            gobbler.TakeDamage(damage);
            Destroy(bottleProjectile); 
            BreakBottle(); 
        }

        ScrapionScript scrapion = collision.gameObject.GetComponent<ScrapionScript>(); // Get the Gobbler component from the collision object

        if (scrapion != null){
            scrapion.TakeDamage(damage);
            Destroy(bottleProjectile); 
            BreakBottle(); 
        }
    }

    void BreakBottle()
    {
        //brokenBottl = Instantiate(brokenBottle, bottleLocation.position, bottleLocation.rotation) as GameObject;
        GameObject bottleFragments = Instantiate(brokenBottle, bottleProjectile.transform.position, bottleProjectile.transform.rotation);
        
        foreach (Transform t in bottleFragments.transform){
            var rb = t.GetComponent<Rigidbody>();

            if (rb != null){
                rb.AddExplosionForce(bottleBreakForce, bottleProjectile.transform.position, 5);
            }
        }

        Destroy(bottleFragments, 2);
    }
}
