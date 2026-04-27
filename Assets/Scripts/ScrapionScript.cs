using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ScrapionScript : MonoBehaviour
{
    [Header("Scrapion Stats")]
    public float moveSpeed;
    public float attackRange;
    public float attackCooldown;
    public int scrapionDamage;
    public int maxHealth;
    int currentHealth;
    public int materialsDropped;
    bool readyToAttack;
    bool isAlive;
    bool isMoving;

    [Header("References")]
    public GameObject scrapion;
    public Transform orientation;
    public Transform scrapionTransform;
    Rigidbody rigidbody;
    private Animator anim;
    Transform playerTransform;
    public AudioSource attackSound;
    public AudioSource walkSound;
    public AudioSource dieSound;

    [Header("Other")]
    Vector3 moveDirection;
    float distanceFromPlayer;
    bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(BeginAttacking), attackCooldown);
        anim = gameObject.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        isAlive = true;
        isMoving = false;
        isWalking = true;
    }

    // Update is called once per frame
void Update()
{
    GameObject player = GameObject.FindWithTag("Player");
    
    if (player != null)
    {
        playerTransform = player.transform;
        
        if (isAlive)
        { 
            RotateTowardsPlayer();

            Vector3 playerLocation = playerTransform.position;
            Vector3 scrapionLocation = scrapionTransform.position;

            float distanceFromPlayer = Vector3.Distance(scrapionLocation, playerLocation);

            if (attackRange < distanceFromPlayer)
            {
                isWalking = true;
                Vector3 direction = (playerLocation - scrapionLocation).normalized;
                rigidbody.velocity = direction * moveSpeed;
                Walk();
            }

            if (readyToAttack && attackRange >= distanceFromPlayer)
            {
                isWalking = false;
                Attack();
            }

            if (currentHealth <= 0)
            {
                isWalking = false;
                Die();
                isAlive = false;
            }
        }
    }
}

    void RotateTowardsPlayer(){
        Vector3 rotateDirection = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(rotateDirection.x, 0, rotateDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    void BeginAttacking(){
        readyToAttack = true;
    }

    void Walk(){
        anim.Play("Base Layer.Scrapion|Walk");
    }

    void Attack(){
        readyToAttack = false;
        attackSound.Play();
        anim.Play("Base Layer.Scrapion|Attack");
        Invoke(nameof(BeginAttacking), attackCooldown);
        StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage(){
        yield return new WaitForSeconds(0.6f);
        FindObjectOfType<TowerScript>().TakeDamage(scrapionDamage);
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
    }
    void Die(){
        dieSound.Play();
        FindObjectOfType<EnemySpawner>().numEnemiesKilled += 1;
        FindObjectOfType<PlayerScript>().recieveMaterials(materialsDropped);
        FindObjectOfType<EnemySpawner>().numEnemies -= 1;
        Destroy(gameObject);
    }
}
