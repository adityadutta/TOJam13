using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour {

    public GameObject door;

    public Vector3 doorHeight;

    public float doorLiftSpeed;

    //sound
    AudioManager audioManager;
    public string doorSwitchSound;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audioManager.PlaySound(doorSwitchSound);
            door.transform.position = Vector3.Lerp(door.transform.position, door.transform.position + doorHeight, doorLiftSpeed);
            transform.position -= new Vector3(0.0f, 0.4f, 0.0f);
        }
    }
}
