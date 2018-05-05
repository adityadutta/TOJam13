using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject.GetComponent<Player>().stats.CurHealth);
            GetComponentInParent<Enemy>().Die();
        }
    }
}
