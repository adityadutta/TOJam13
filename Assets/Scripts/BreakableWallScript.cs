using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallScript : MonoBehaviour {

    public Transform breakwallParticles;

    //sound
    AudioManager audioManager;
    public string breakwallSound;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.currentState == PlayerStates.Hard)
            {
                audioManager.PlaySound(breakwallSound);
                Transform clone = Instantiate(breakwallParticles, transform.position, transform.rotation) as Transform;
                Destroy(this.gameObject);
                Destroy(clone.gameObject, 3f);
            }
        }
    }
}
