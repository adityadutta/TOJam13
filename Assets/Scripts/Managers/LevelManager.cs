using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    bool isMenu = true;
    bool isGame = false;
    bool isInfo = false;

    public GameObject infoPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (isMenu || GameManager.Instance.isGameOver)
            {
                isMenu = false;
                isGame = true;
                SceneManager.LoadScene("01_Level");
            }
        }

        if (Input.GetButtonDown("Quit"))
        {
            if (isGame)
            {
                isMenu = true;
                isGame = false;
                SceneManager.LoadScene("MainMenu");
            }
            else if (isInfo)
            {
                isMenu = true;
                isGame = false;
                isInfo = false;
                infoPanel.SetActive(false);
            }
            else
            {
                Debug.Log("QUIT");
                Application.Quit();
            }

        }
    }

    public void Info()
    {
        isInfo = true;
        infoPanel.SetActive(true);
    }
}
