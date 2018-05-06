using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondScript : MonoBehaviour {

    public int points = 200;

    //sound
    AudioManager audioManager;
    public string coinSound;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
    }
    private void Update()
    {
        transform.Rotate(new Vector3(45.0f, 0.0f, 45.0f) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audioManager.PlaySound(coinSound);
            Player player = other.GetComponent<Player>();
            player.stats.CurCoins++;
            player.stats.CurScore += points;
            Destroy(this.gameObject);
        }
    }
}
