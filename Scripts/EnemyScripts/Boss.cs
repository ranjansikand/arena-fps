using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float initialDelay = 12f;

    bool takesDamage, readyToAttack;

    [SerializeField] Transform player;


    // health stuff
    [SerializeField] int maximumHealth = 4000;
    int currentHealth;

    [SerializeField] GameObject deathParticle;

    [SerializeField] GameObject hpBar;
    private HealthBarScript healthBar;

    // attacking stuff
    [SerializeField] float timeBetweenNormalAttacks, timeAfterHoming, attackForce;
    bool alreadyAttacked;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject homingProjectile;

    [SerializeField] Transform fireFromPoint;
    [SerializeField] Vector3 firingAdjustment = new Vector3(0, -1, 0);

    void Start()
    {
        Invoke("DelayedEnlarge", initialDelay);

        Invoke("PrepareForBattle", initialDelay * 1.5f);

        currentHealth = maximumHealth;

        healthBar = hpBar.GetComponent<HealthBarScript>();
        healthBar.SetMaxHealth(maximumHealth);

        hpBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (takesDamage)
        {
            transform.LookAt(player);

            AttackPlayer();
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void AttackPlayer()
    {
        transform.LookAt(player.position + firingAdjustment);

        if (!alreadyAttacked)
        {
            if (Random.Range(1, 5) == 2)
            {
                Instantiate(homingProjectile, fireFromPoint.position + Vector3.down * 3, Quaternion.identity);
                Invoke(nameof(ResetAttack), timeAfterHoming);
            }
            else 
            {
                Rigidbody rb = Instantiate(projectile, fireFromPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * attackForce, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenNormalAttacks);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void DelayedEnlarge()
    {
        StartCoroutine("Enlarge");
    }

    void PrepareForBattle()
    {
        takesDamage = true;

        hpBar.SetActive(true);
    }

    IEnumerator Enlarge()
    {
        for (int i = 0; i < 100; i++)
        {
            transform.localScale += new Vector3(1,1,1);

            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9 && other.gameObject.tag == "Projectile" && takesDamage)
        {
            currentHealth -= other.gameObject.GetComponent<BulletScript>().DamageDealt();
            healthBar.SetHealth(currentHealth);
        }
    }

    void Death()
    {
        if (deathParticle != null)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }

        GameObject.Find("Manager").GetComponent<Management>().EndGame();

        Destroy(gameObject);
    }
}
