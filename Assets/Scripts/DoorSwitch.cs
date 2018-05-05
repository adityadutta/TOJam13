using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour {

    public GameObject door;

    public Vector3 doorHeight;

    public float doorLiftSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            door.transform.position = Vector3.Lerp(door.transform.position, door.transform.position + doorHeight, doorLiftSpeed);
            transform.position -= new Vector3(0.0f, 0.4f, 0.0f);
        }
    }
}
