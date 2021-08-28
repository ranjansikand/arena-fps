using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnemy : MonoBehaviour
{
    [SerializeField] GameObject spawnPoints;
    Deactivate spawnDeactivate;

    [SerializeField] GameObject disappearanceParticle;

    void Start()
    {
        spawnDeactivate = spawnPoints.GetComponent<Deactivate>();
    }

    public void Death()
    {
        spawnDeactivate.DeactivateSpawning();
        if (disappearanceParticle != null)
            Instantiate(disappearanceParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9 && other.gameObject.tag == "Projectile")
        {
            Death();
        }
    }
}
