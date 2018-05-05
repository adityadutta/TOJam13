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
    }

    public Text nameText;
    public Text dialogueText;

    public Queue<string> sentences;

	// Use this for initialization
	void Start ()
    {
        sentences = new Queue<string>();
	}

    private void Update()
    {
        if (Input.anyKeyDown)
            DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
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
