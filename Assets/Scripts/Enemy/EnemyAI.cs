using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Idle,
    Wander,
    Chase
}
public class EnemyAI : MonoBehaviour {

    public States currentState = States.Idle;
    public Transform target;

    private Vector3 startPos;
    private Vector3 endPos;
    public float wanderOffset;
    bool moveRight = true;
    bool moveLeft = false;

    private float chaseRange = 5.0f;

    //Movement stuff
    [HideInInspector]
    public bool facingRight = true;
    private float currentSpeed;
    public float chaseSpeed = 1.0f;
    public float wanderSpeed = 0.5f;

    //components
    private Rigidbody rb;
    private Enemy enemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentSpeed = wanderSpeed;
        startPos = new Vector3(transform.position.x - wanderOffset, transform.position.y, transform.position.z);
        endPos = new Vector3(transform.position.x + wanderOffset, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        UpdateStates();

        switch (currentState)
        {
            case States.Wander:
                Wander();
                break;
            case States.Chase:
                Chase();
                break;
        }

        if (rb.velocity.x > 0.0f && !facingRight)
            Flip();
        else if (rb.velocity.x < 0.0f && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    void UpdateStates()
    {
        if (!enemy.stats.isDead)
        {
            if (target != null)
            {
                float distance = Vector2.Distance(target.position, transform.position);
                if (distance < chaseRange)
                {
                    currentState = States.Chase;
                }
                else
                {
                    currentState = States.Wander;
                }
            }
        }     
    }

    void Wander()
    {
        if(transform.position.x > endPos.x && moveRight)
        {
            currentSpeed = -1.0f;
            moveRight = false;
            moveLeft = true;
        }
        else if(transform.position.x < startPos.x && moveLeft)
        {
            currentSpeed = 1.0f;
            moveRight = true;
            moveLeft = false;
        }
        rb.velocity = new Vector3(currentSpeed, rb.velocity.y, rb.velocity.z);
    }

    void Chase()
    {
        if(target != null)
        {
            currentSpeed = chaseSpeed;
            Vector3 direction = target.position - transform.position;
            Vector3 hDirection = new Vector3(direction.x, 0.0f, 0.0f);
            Vector3 velocity = hDirection * currentSpeed;
            rb.velocity = velocity;
        }
    }
}
