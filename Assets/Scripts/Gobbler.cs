using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobbler : MonoBehaviour
{
    [Header("Gobbler Stats")]
    public float walkCooldown;
    bool readyToWalk;
    public float moveSpeed;
    public float moveForce;
    public float attackRange;
    public float attackCooldown;
    public int gobblerDamage;
    public int maxHealth;
    int currentHealth;
    public int materialsDropped;
    bool readyToAttack;
    bool isAlive;

    [Header("References")]
    public GameObject gobbler;
    public Transform orientation;
    public Transform gobblerTransform;
    Rigidbody rigidbody;
    private Animator anim;
    Transform playerTransform;
    public AudioSource walkSound;
    public AudioSource biteSound;
    public AudioSource dieSound;

    [Header("Other")]
    Vector3 moveDirection;
    float distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(BeginWalking), walkCooldown);
        Invoke(nameof(BeginBiting), attackCooldown);
        anim = gameObject.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null){
            playerTransform = player.transform;
        }

        if (isAlive){ 
            RotateTowardsPlayer();

            Vector3 playerLocation = playerTransform.transform.position;
            Vector3 gobblerLocation = gobblerTransform.transform.position;

            float distanceFromPlayer = Vector3.Distance(gobblerLocation, playerLocation);

            if (readyToWalk && attackRange < distanceFromPlayer){
                Walk();
            }

            if (readyToAttack && attackRange >= distanceFromPlayer){
                biteSound.Play();
                Bite();
            }

            if (currentHealth <= 0){
                dieSound.Play();
                StartCoroutine(Die());
                isAlive = false;
            }
        }
    }

    void Walk(){
        readyToWalk = false;
        Dash();
        anim.Play("Base Layer.Gobbler|Walk");
        walkSound.Play();
        Invoke(nameof(BeginWalking), walkCooldown);
    }

    void BeginWalking(){
        readyToWalk = true;
    }

    void Dash(){
        Vector3 moveDirection = orientation.forward;
        rigidbody.AddForce(moveDirection * moveForce, ForceMode.Impulse);
    }

    void RotateTowardsPlayer(){
        Vector3 rotateDirection = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(rotateDirection.x, 0, rotateDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void BeginBiting(){
        readyToAttack = true;
    }
    void Bite(){
        readyToAttack = false;
        anim.Play("Base Layer.Gobbler|Bite");
        Invoke(nameof(BeginBiting), attackCooldown);
        StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage(){
        yield return new WaitForSeconds(0.6f);
        FindObjectOfType<TowerScript>().TakeDamage(gobblerDamage);
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
    }
    IEnumerator Die(){
        anim.Play("Base Layer.Gobbler|Die");
        FindObjectOfType<EnemySpawner>().numEnemiesKilled += 1;
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<PlayerScript>().recieveMaterials(materialsDropped);
        FindObjectOfType<EnemySpawner>().numEnemies -= 1;
        Destroy(gameObject);
    }
}
