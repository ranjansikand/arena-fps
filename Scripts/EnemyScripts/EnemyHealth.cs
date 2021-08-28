using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maximumHealth = 100;
    int currentHealth;

    [SerializeField] int pointsPerKill = 50;
    private Score score;

    [SerializeField] bool deathAnim = false;
    [SerializeField] GameObject deathParticle;


    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maximumHealth;

        score = GameObject.Find("Scoreboard").GetComponent<Score>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            if (deathAnim) {
                AnimatedDeath();
            }
            else {
                Death();
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9 && other.gameObject.tag == "Projectile")
        {
            currentHealth -= other.gameObject.GetComponent<BulletScript>().DamageDealt();
        }
    }

    void Death()
    {
        if (deathParticle != null)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }

        score.UpdateScore(pointsPerKill);
        Destroy(gameObject);
    }

    public void AnimatedDeath()
    {
        Debug.Log("Animated Death");
    }
}
