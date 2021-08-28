using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] bool destroyWithoutImpact = false;

    [SerializeField] 
    private GameObject particleEffect;
    [SerializeField] 
    private bool delayedEffect = false;
    [SerializeField, Range(0f, 5f)] 
    private float delayLength = 0.25f;


    [SerializeField]
    int damagePerHit = 25;

    void Start()
    {
        if (destroyWithoutImpact)
        {
            Invoke("DelayedDestruction", delayLength);
        }
    }

    void OnCollisionEnter()
    {
        if (particleEffect != null) {
            Instantiate(particleEffect, transform.position, Quaternion.identity);
        }
        
        if (delayedEffect) {
            Invoke("DelayedDestruction", delayLength);
        } else {
            Destroy(gameObject);
        }
    }

    void DelayedDestruction()
    {
        Destroy(gameObject);
    }

    public int DamageDealt()
    {
        return damagePerHit;
    }
}
