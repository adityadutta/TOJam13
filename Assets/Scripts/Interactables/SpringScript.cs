using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour {

    public float bounceForce = 600.0f;

    //sound
    AudioManager audioManager;
    public string springSound;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            audioManager.PlaySound(springSound);
            if(other.gameObject.GetComponent<Player>().currentState == PlayerStates.Bouncy)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * bounceForce / 2.0f);
            }
            else
                other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * bounceForce);  
        }
    }
}
