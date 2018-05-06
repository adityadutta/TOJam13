using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public Text highScore;
    public Text bestTime;

    private void Start()
    {
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
       
        float duration = PlayerPrefs.GetFloat("BestTime");
        string minutes = ((int)duration / 60).ToString("00");
        string seconds = (duration % 60).ToString("00.00");
        bestTime.text = "Best Time:" + minutes + ":" + seconds;
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Cleared");
        SceneManager.LoadScene("MainMenu");
    }
}
