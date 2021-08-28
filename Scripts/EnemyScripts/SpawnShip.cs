using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShip : MonoBehaviour
{
    [SerializeField] int maximumHealth = 600;
    int currentHealth;

    [SerializeField] GameObject exhaustParticle;
    GameObject eP1, eP2, eP3;

    [SerializeField] Transform exhaust1, exhaust2, exhaust3;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maximumHealth;

        StartExhaust();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9 && other.gameObject.tag == "Projectile")
        {
            currentHealth -= other.gameObject.GetComponent<BulletScript>().DamageDealt();
        }
    }

    void StartExhaust()
    {
        eP1 = Instantiate(exhaustParticle, exhaust1.position, Quaternion.identity);
        eP2 = Instantiate(exhaustParticle, exhaust2.position, Quaternion.identity);
        eP3 = Instantiate(exhaustParticle, exhaust3.position, Quaternion.identity);   
    }

    void Death()
    {
        Destroy(eP1);
        Destroy(eP2);
        Destroy(eP3);

        Debug.Log("Ship destroyed");
        rb.useGravity = true;

        GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().StopSpawning();
    }


}
