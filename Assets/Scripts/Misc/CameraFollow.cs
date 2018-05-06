using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 10.0f;

    public Vector3 offset;
    float nextTimeToSearch = 0;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }
        else
        {
            Vector3 newPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        }   
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                target = searchResult.transform;
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
