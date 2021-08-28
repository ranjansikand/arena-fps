using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepNextScene : MonoBehaviour
{
    [SerializeField] GameObject manager;

    void OnCollisionEnter()
    {
        manager.GetComponent<Management>().LoadBossBattle();
    }
}
