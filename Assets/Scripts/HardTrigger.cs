using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardTrigger : MonoBehaviour {

    //sound
    public string hardSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.PlaySound(hardSound);
            GameManager.Instance.isHard = true;
        }
    }
}
