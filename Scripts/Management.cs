using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Management : MonoBehaviour
{
    [SerializeField] float loadingLength = 5f;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            Invoke("StartGame", loadingLength);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (SceneManager.GetActiveScene().name == "TitleScreen")
            {
                Debug.Log("Exit game");
                Application.Quit();
            }
            else {
                SceneManager.LoadScene("TitleScreen");
            }
        }
    }

    public void LoadScreen()
    {
        SceneManager.LoadScene("Loading");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Play");
    }

    public void LoadBossBattle()
    {
        SceneManager.LoadScene("Boss");
    }

    public void EndGame()
    {
        Invoke("LoadCredit", 5.5f);
    }

    void LoadCredit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void LoadControlScene()
    {
        SceneManager.LoadScene("Controls");
    }
}
