using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private EnemyDetection enemyDetection;
    private bool firing = false;

    // Bullet variables
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform fireFromPoint;

    [SerializeField] float fireForce = 1f;


    // Start is called before the first frame update
    void Start()
    {
        enemyDetection = GetComponent<EnemyDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Time.deltaTime % 2 == 1)
        // {
        //    if (enemyDetection.Fire() && !firing)
        //     {
        //         firing = true;
    
        //         StartCoroutine("Shooting");
        //     }
        //     else
        //     {
        //         firing = false;
    
        //         StopCoroutine("Shooting");
        //     }
        // } 

        if (firing)
            Debug.Log("Somehow its firing");
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, fireFromPoint.position, transform.rotation);

            bullet.transform.forward = transform.forward;
            bullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * fireForce, ForceMode.Impulse);

            yield return new WaitForSeconds(3);
        }
    }
}
