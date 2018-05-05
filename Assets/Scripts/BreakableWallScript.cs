using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallScript : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.currentState == PlayerStates.Hard)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
