using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {

    public static UIScript instance;

    private Player player;

    public Text scoreText;
    public Text coinText;
    public Text timeText;
   

    private void Awake()
    {
        if(instance == null)
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
        scoreText.text = "Score: " + player.stats.CurScore;
        coinText.text = "Diamonds: " + player.stats.CurCoins;
        timeText.text = "Time:" + GameManager.Instance.minutes + ":" + GameManager.Instance.seconds;
    }
}
