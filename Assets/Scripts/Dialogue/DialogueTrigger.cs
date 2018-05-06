using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TriggerDialogue();
            Destroy(this.gameObject);
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
