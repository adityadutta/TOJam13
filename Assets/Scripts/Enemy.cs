using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats
    {
        public int damage;

       public bool isDead;

        public void Init()
        {
            isDead = false;
            damage = 10;
        }
    }

    public EnemyStats stats = new EnemyStats();

    public Transform enemyDeathParticles;

    private void Start()
    {
        stats.Init();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
             other.gameObject.GetComponent<Player>().DamagePlayer(stats.damage);
        }
    }

    public void Die()
    {      
        GetComponent<EnemyAI>().currentState = States.Idle;
        GetComponent<CapsuleCollider>().enabled = false;
        GameManager.KillEnemy(this);
    }
}
