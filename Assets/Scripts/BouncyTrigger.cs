using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.isBouncy = true;
        }
    }
}
