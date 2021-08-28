using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPoint : MonoBehaviour
{
    [SerializeField] float minSpeed = 1f, maxSpeed = 5f;
    float speed;

    [SerializeField] float maxDistance = 4f;

    [SerializeField] bool tracksPlayer = false;
    [SerializeField] bool goToBoss = true;

    Transform target;

    void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        if (tracksPlayer) {
            target = GameObject.Find("Player").transform;
        }

        if (goToBoss) {
            target = GameObject.FindWithTag("Boss").transform;
        }
    }

    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) > maxDistance)
        {
            float step =  speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    
            // if (Vector3.Distance(transform.position, target.position) < 0.001f)
            // {
            //     target.position *= -1.0f;
            // }
        }
    }
}
