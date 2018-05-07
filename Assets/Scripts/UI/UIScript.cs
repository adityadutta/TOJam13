using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    public static UIScript instance;

    private Player player;

    public Text scoreText;
    public Text coinText;
    public Text timeText;

    private GameObject playerBody;
    float nextTimeToSearch = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        FindPlayer();
        if (player == null)
        {
            FindPlayer();
        }

        scoreText.text = "Score: " + player.stats.CurScore;
        coinText.text = "Diamonds: " + player.stats.CurCoins;
        timeText.text = "Time:" + GameManager.Instance.minutes + ":" + GameManager.Instance.seconds;

    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                playerBody = searchResult.gameObject;
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
