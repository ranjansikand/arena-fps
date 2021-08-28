using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maximumHealth = 100;
    int currentHealth;

    [SerializeField] TextMeshProUGUI gameOverDisplay;

    [SerializeField] GameObject hpBar;
    private HealthBarScript healthBar;

    [SerializeField] GameObject manager;
    [SerializeField, Range(0.1f, 5f)] float loadingDelay = 1.5f;

    void Start()
    {
        healthBar = hpBar.GetComponent<HealthBarScript>();
        currentHealth = maximumHealth;

        healthBar.SetMaxHealth(maximumHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 11 && other.gameObject.tag == "Projectile")
        {
            currentHealth -= other.gameObject.GetComponent<BulletScript>().DamageDealt();
            healthBar.SetHealth(currentHealth);
        }
        else if (other.gameObject.tag == "Hazard") 
        {
            Debug.Log("Hazard encountered");
        }
        else if (other.gameObject.tag == "Weapon")
        {
            Debug.Log("Hit with weapon!");
        }
    }


    void Death() 
    {
        if (gameOverDisplay != null) {
            gameOverDisplay.SetText("YOU DIED");
        }

        GetComponent<ProjectileGun>().DisableShooting();

       Invoke("SwitchScenes", loadingDelay);
    }

    void SwitchScenes()
    {
        manager.GetComponent<Management>().LoadScreen();
    }
}
