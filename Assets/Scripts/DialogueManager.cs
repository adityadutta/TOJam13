using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager instance;
    public Animator dialogueBoxAnim;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        audioManager = AudioManager.Instance;
    }

    public Text nameText;
    public Text dialogueText;

    public Queue<string> sentences;

    //sound
    AudioManager audioManager;
    public string dialogueSound;

    // Use this for initialization
    void Start ()
    {
        sentences = new Queue<string>();
	}

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            DisplayNextSentence();
        }
          
    }

    public void StartDialogue(Dialogue dialogue)
    {
        audioManager.PlaySound(dialogueSound);
        dialogueBoxAnim.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        audioManager.PlaySound(dialogueSound);

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueBoxAnim.SetBool("isOpen", false);
    }
	
}
