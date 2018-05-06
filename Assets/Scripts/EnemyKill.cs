using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        { 
            other.gameObject.GetComponent<Player>().stats.CurScore += 200;
            GetComponentInParent<Enemy>().Die();
        }
    }
}
