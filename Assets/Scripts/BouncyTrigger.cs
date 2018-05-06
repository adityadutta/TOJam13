using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyTrigger : MonoBehaviour {

    public string bouncySound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AudioManager.Instance.PlaySound(bouncySound);
            GameManager.Instance.isBouncy = true;
        }
    }
}
