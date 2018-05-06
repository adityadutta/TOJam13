using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //speed stuff
    public float speed = 5.0f;

    //physics stuff
    private Rigidbody rb;

    //jump stuff
    public bool grounded = true;
    public float jumpForce = 300.0f;

    //sound
    AudioManager audioManager;
    public string jumpSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = AudioManager.Instance;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal * speed,  rb.velocity.y, 0.0f) ;
        rb.velocity = movement;

        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                audioManager.PlaySound(jumpSound);
                rb.AddForce(Vector3.up * jumpForce);
                grounded = false;
            }      
        }
            
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
